using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerState
{
    public PlayerState_Idle(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data, PlayerManager manager) : base(stateMachine, data, manager)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if(Data.HasMoveInput)
        {
            StateMachine.ChangeState(PlayerStates.Move);
        }
    }

    public override void Exit()
    {
    }
}
