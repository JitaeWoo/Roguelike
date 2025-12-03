using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine<PlayerStates> _stateMachine = new StateMachine<PlayerStates>();

    private void Awake()
    {
        PlayerRuntimeData data = GetComponent<PlayerRuntimeData>();

        _stateMachine.AddState(PlayerStates.Idle, new PlayerState_Idle(_stateMachine, data));
        _stateMachine.AddState(PlayerStates.Move, new PlayerState_Move(_stateMachine, data));
        _stateMachine.AddState(PlayerStates.Dash, new PlayerState_Dash(_stateMachine, data));

        _stateMachine.ChangeState(PlayerStates.Idle);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}
