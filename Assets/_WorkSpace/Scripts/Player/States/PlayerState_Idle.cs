using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerState_Idle : PlayerState
{

    public PlayerState_Idle(StateMachine<PlayerStates> stateMachine) : base(stateMachine)
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
