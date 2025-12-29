using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class MonsterState : BaseState<MonsterStates>
{
    protected MonsterData Data;

    [Inject]
    private void Init(MonsterData data)
    {
        Data = data;
    }

    public MonsterState(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }
}
