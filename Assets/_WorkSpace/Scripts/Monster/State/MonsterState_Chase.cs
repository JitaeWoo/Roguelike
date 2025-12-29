using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterState_Chase : MonsterState
{
    private MonsterChaseCollider _chase;
    private Rigidbody2D _rigid;
    private PlayerManager _playerManager;

    [Inject]
    private void Init(MonsterChaseCollider chase, Rigidbody2D rigid, PlayerManager playerManager)
    {
        _chase = chase;
        _rigid = rigid;
        _playerManager = playerManager;
    }

    public MonsterState_Chase(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        _chase.OnExit
            .TakeWhile(_ => StateMachine.CurStateEnum == MonsterStates.Chase)
            .Subscribe(_ => StateMachine.ChangeState(MonsterStates.Idle))
            .AddTo(Data.gameObject);
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        Vector2 dir = _playerManager.Position - Data.transform.position;
        dir = dir.normalized;

        _rigid.velocity = dir * Data.MoveSpeed;
    }

    public override void Exit()
    {
    }
}
