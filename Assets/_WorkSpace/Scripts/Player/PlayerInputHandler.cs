using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerRuntimeData _playerRuntimeData;
    private SkillManager _skillManager;
    PlayerInputAsset _asset;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _attackAction;

    [Inject]
    private void Init(PlayerRuntimeData data, SkillManager skillManager)
    {
        _playerRuntimeData = data;
        _skillManager = skillManager;
    }

    private void Awake()
    {
        _asset = new PlayerInputAsset();

        _moveAction = _asset.Player.Move;
        _dashAction = _asset.Player.Dash;
        _attackAction = _asset.Player.Attack;
    }

    private void OnEnable()
    {
        _asset.Enable();
    }

    private void OnDisable()
    {
        _asset.Disable();
    }

    private void OnDestroy()
    {
        _asset.Dispose();
    }

    private void Start()
    {
        MoveInput();
        DashInput();
        AttackInput();
    }

    private void MoveInput()
    {
        // 입력이 변화할 때마다 _moveDir을 기록한다.
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.performed += f,
            f => _moveAction.performed -= f)
            .Select(ctx => ctx.ReadValue<Vector2>())
            .Subscribe(v => _playerRuntimeData.MoveDir = v)
            .AddTo(this);

        // 입력이 끊기면 HasMoveInput을 false로.
        Observable<InputAction.CallbackContext> cancelStream = Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.canceled += f,
            f => _moveAction.canceled -= f);
        cancelStream.Subscribe(_ => _playerRuntimeData.HasMoveInput = false)
            .AddTo(this);

        //// 입력이 지속되는 동안 HasMoveInput을 ture로.
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.started += f,
            f => _moveAction.started -= f)
            .Subscribe(moveDir => _playerRuntimeData.HasMoveInput = true)
            .AddTo(this);
    }

    private void DashInput()
    {
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _dashAction.performed += f,
            f => _dashAction.performed -= f)
            .Subscribe(_ => _skillManager.Dash.Trigger())
            .AddTo(this);
    }

    private void AttackInput()
    {
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _attackAction.performed += f,
            f => _attackAction.performed -= f)
            .Subscribe(_ => _skillManager.UseSkill(0))
            .AddTo(this);
    }
}
