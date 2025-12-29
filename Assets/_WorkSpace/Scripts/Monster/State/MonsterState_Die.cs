using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterState_Die : MonsterState
{

    private Collider2D _collider;
    private SpriteRenderer _renderer;

    [Inject]
    private void Init(Collider2D collider2D, SpriteRenderer renderer)
    {
        _collider = collider2D;
        _renderer = renderer;
    }

    public MonsterState_Die(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Data.IsDead.Value = true;

        _collider.enabled = false;

        _renderer.material
            .DOFloat(0, "_SplitValue", 1f).OnComplete(() => Object.Destroy(Data.gameObject))
            .SetLink(Data.gameObject);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
