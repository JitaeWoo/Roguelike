using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerRuntimeData _playerRuntimeData;
    private InputAction _moveAction;

    private void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        _moveAction = _playerInput.actions["Move"];
        if(_playerRuntimeData == null)
        {
            _playerRuntimeData = GetComponent<PlayerRuntimeData>();
        }
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Start()
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
}
