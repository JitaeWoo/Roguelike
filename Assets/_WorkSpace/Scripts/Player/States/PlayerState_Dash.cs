using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class PlayerState_Dash : PlayerState
{
    private Vector2 _dashDir;
    public PlayerState_Dash(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data, PlayerManager manager) : base(stateMachine, data, manager)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        _dashDir = Data.MoveDir;
        DashTimer().Forget();
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        Data.Velocity = _dashDir * PlayerManager.Data.DashSpeed;
    }

    public override void Exit()
    {
        Data.Velocity = Vector2.zero;
    }

    private async UniTaskVoid DashTimer()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        if (Data == null || StateMachine.CurStateEnum != PlayerStates.Dash) return;

        StateMachine.ChangeState(PlayerStates.Idle);
    }
}
