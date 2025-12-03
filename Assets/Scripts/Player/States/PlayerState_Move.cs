using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : PlayerState
{
    public PlayerState_Move(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data) : base(stateMachine, data)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
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
        Data.Velocity = Data.MoveDir * Manager.Player.Data.MoveSpeed;
    }

    public override void Exit()
    {
        Data.Velocity = Vector2.zero;
    }
}
