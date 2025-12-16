using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerState_Move : PlayerState
{
    public PlayerState_Move(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data, PlayerManager manager) : base(stateMachine, data, manager)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        Data.IsMove.Value = true;
    }

    public override void Update()
    {
        if(!Data.HasMoveInput)
        {
            StateMachine.ChangeState(PlayerStates.Idle);
        }
    }

    public override void FixedUpdate()
    {
        Data.Velocity = Data.MoveDir * PlayerManager.Data.MoveSpeed;
    }

    public override void Exit()
    {
        Data.Velocity = Vector2.zero;
        Data.IsMove.Value = false;
    }
}
