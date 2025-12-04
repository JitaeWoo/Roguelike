using R3;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerRuntimeData _playerRuntimeData;

    private StateMachine<PlayerStates> _stateMachine = new StateMachine<PlayerStates>();

    private void Awake()
    {
        if (_playerRuntimeData == null)
        {
            _playerRuntimeData = GetComponent<PlayerRuntimeData>();
        }

        _stateMachine.AddState(PlayerStates.Idle, new PlayerState_Idle(_stateMachine, _playerRuntimeData));
        _stateMachine.AddState(PlayerStates.Move, new PlayerState_Move(_stateMachine, _playerRuntimeData));
        _stateMachine.AddState(PlayerStates.Dash, new PlayerState_Dash(_stateMachine, _playerRuntimeData));

        _stateMachine.ChangeState(PlayerStates.Idle);
    }

    private void Start()
    {
        _playerRuntimeData.Dash.Event
            .Subscribe(_ => Dash())
            .AddTo(this);
    }

    private void Dash()
    {
        _stateMachine.ChangeState(PlayerStates.Dash);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}
