using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle, Move, Dash, Size
}
public abstract class PlayerState : BaseState<PlayerStates>
{
    protected PlayerRuntimeData Data;

    public PlayerState(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data) : base(stateMachine)
    {
        Data = data;
    }
}
