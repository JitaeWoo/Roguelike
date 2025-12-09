using R3;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayerRuntimeData _playerRuntimeData;
    private StateMachine<PlayerStates> _stateMachine = new StateMachine<PlayerStates>();

    [Inject]
    private void init(PlayerManager manager, PlayerRuntimeData data)
    {
        _playerManager = manager;
        _playerRuntimeData = data;
    }

    private void Awake()
    {
        _stateMachine.AddState(PlayerStates.Idle, new PlayerState_Idle(_stateMachine, _playerRuntimeData, _playerManager));
        _stateMachine.AddState(PlayerStates.Move, new PlayerState_Move(_stateMachine, _playerRuntimeData, _playerManager));
        _stateMachine.AddState(PlayerStates.Dash, new PlayerState_Dash(_stateMachine, _playerRuntimeData, _playerManager));

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
