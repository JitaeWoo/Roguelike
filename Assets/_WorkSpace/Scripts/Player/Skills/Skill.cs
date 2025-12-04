using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;

public abstract class Skill
{
    protected Transform _player;

    protected float Cooldown;
    private bool _isOnCooldown;
    public bool CanUse = true;
    private Subject<Unit> _event = new Subject<Unit>();
    public Observable<Unit> Event { get => _event.AsObservable(); }

    public Skill(Transform player = null)
    {
        _player = player;
    }

    public void Trigger()
    {
        if (!CanUse || _isOnCooldown) return;

        _event.OnNext(Unit.Default);
        ActivateSkill();

        StartCooldown().Forget();
    }

    protected abstract void ActivateSkill();

    private async UniTaskVoid StartCooldown()
    {
        _isOnCooldown = true;
        await UniTask.Delay(TimeSpan.FromSeconds(Cooldown));
        _isOnCooldown = false;
    }
}
