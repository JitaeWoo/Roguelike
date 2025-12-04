using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerState_Dash : PlayerState
{
    private Vector2 _dashDir;
    public PlayerState_Dash(StateMachine<PlayerStates> stateMachine, PlayerRuntimeData data) : base(stateMachine, data)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        _dashDir = Data.MoveDir;
        DashTimer().Forget();
        DashCooldown().Forget();
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        Data.Velocity = _dashDir * Manager.Player.Data.DashSpeed;
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

    private async UniTaskVoid DashCooldown()
    {
        Data.IsDashOnCooldown = true;
        await UniTask.Delay(TimeSpan.FromSeconds(Data.DashCooldown));

        if (Data == null) return;

        Data.IsDashOnCooldown = false;
    }
}
