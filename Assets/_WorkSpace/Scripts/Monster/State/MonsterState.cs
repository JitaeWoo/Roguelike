using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState : BaseState<MonsterStates>
{
    public MonsterState(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }
}
