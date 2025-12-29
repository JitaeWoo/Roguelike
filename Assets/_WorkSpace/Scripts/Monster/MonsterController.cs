using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MonsterStates
{
    Idle, Chase, Die, Size
}

public class MonsterController : MonoBehaviour
{
    private StateMachine<MonsterStates> _stateMachine = new StateMachine<MonsterStates>();

    private DiContainer _diContainer;

    private void Init(DiContainer di)
    {
        _diContainer = di;
    }

    private void Awake()
    {
        MonsterState_Idle idle = new MonsterState_Idle(_stateMachine);
        MonsterState_Chase chase = new MonsterState_Chase(_stateMachine);
        MonsterState_Die die = new MonsterState_Die(_stateMachine);

        _stateMachine.AddState(MonsterStates.Idle, idle);
        _diContainer.Inject(idle);
        _stateMachine.AddState(MonsterStates.Chase, chase);
        _diContainer.Inject(chase);
        _stateMachine.AddState(MonsterStates.Die, die);
        _diContainer.Inject(die);

        _stateMachine.ChangeState(MonsterStates.Idle);
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
