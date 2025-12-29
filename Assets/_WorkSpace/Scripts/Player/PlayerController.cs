using R3;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SkillManager _skillManager;
    private StateMachine<PlayerStates> _stateMachine = new StateMachine<PlayerStates>();
    private DiContainer _diContainer;

    [Inject]
    private void init(SkillManager skillManager, DiContainer di)
    {
        _skillManager = skillManager;
        _diContainer = di;
    }

    private void Awake()
    {
        PlayerState_Idle idle = new PlayerState_Idle(_stateMachine);
        PlayerState_Move move = new PlayerState_Move(_stateMachine);
        PlayerState_Dash dash = new PlayerState_Dash(_stateMachine);

        _stateMachine.AddState(PlayerStates.Idle, idle);
        _diContainer.Inject(idle);
        _stateMachine.AddState(PlayerStates.Move, move);
        _diContainer.Inject(move);
        _stateMachine.AddState(PlayerStates.Dash, dash);
        _diContainer.Inject(dash);

        _stateMachine.ChangeState(PlayerStates.Idle);
    }

    private void Start()
    {
        _skillManager.Dash.Event
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
