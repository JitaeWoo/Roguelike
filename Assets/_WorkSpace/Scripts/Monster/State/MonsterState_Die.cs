using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterState_Die : MonsterState
{
    private Collider2D _collider;
    private AudioManager _audioManager;

    [Inject]
    private void Init(Collider2D collider2D, AudioManager audioManager)
    {
        _collider = collider2D;
        _audioManager = audioManager;
    }

    public MonsterState_Die(StateMachine<MonsterStates> stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Data.IsDead.Value = true;

        _collider.enabled = false;

        _audioManager.SfxPlay(Data.DeadSfx, Data.transform);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
