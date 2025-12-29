using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum PlayerStates
{
    Idle, Move, Dash, Size
}
public abstract class PlayerState : BaseState<PlayerStates>
{
    protected PlayerRuntimeData Data;
    protected PlayerManager PlayerManager;

    [Inject]
    private void Init(PlayerRuntimeData data, PlayerManager manager)
    {
        Data = data;
        PlayerManager = manager;
    }

    protected PlayerState(StateMachine<PlayerStates> stateMachine) : base(stateMachine)
    {
    }
}
