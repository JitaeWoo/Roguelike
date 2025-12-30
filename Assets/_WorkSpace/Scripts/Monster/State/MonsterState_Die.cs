using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterState_Die : MonsterState
{
    private Collider2D _collider;

    [Inject]
    private void Init(Collider2D collider2D)
    {
        _collider = collider2D;
    }

    public MonsterState_Die(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Data.IsDead.Value = true;

        _collider.enabled = false;
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
