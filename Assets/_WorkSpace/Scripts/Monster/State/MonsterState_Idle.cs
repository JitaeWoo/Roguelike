using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterState_Idle : MonsterState
{
    private MonsterChaseCollider _chase;

    [Inject]
    private void Init(MonsterChaseCollider chase)
    {
        _chase = chase;
    }


    public MonsterState_Idle(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _chase.OnEnter
            .TakeWhile(_ => StateMachine.CurStateEnum == MonsterStates.Idle)
            .Subscribe(_ => StateMachine.ChangeState(MonsterStates.Chase))
            .AddTo(Data.gameObject);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

}
