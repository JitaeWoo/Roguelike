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
    protected PlayerManager PlayerManager;

    public PlayerState(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data, PlayerManager playerManager) : base(stateMachine)
    {
        Data = data;
        PlayerManager = playerManager;
    }
}
