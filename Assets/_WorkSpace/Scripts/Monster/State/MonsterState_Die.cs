using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Die : MonsterState
{
    public MonsterState_Die(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Data.IsDead.Value = true;
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
