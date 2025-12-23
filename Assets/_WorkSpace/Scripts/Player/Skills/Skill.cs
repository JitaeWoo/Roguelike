using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using Zenject;

public abstract class Skill : MonoBehaviour
{
    protected SkillData Data;

    private bool _isOnCooldown;
    public bool CanUse = true;
    private Subject<Unit> _event = new Subject<Unit>();
    public Observable<Unit> Event { get => _event.AsObservable(); }

    protected PlayerManager Player;
    protected DiContainer DiContainer;

    [Inject]
    private void Init(PlayerManager playerManager, DiContainer di)
    {
        Player = playerManager;
        DiContainer = di;
    }

    public void SetData(SkillData data)
    {
        Data = data;
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
        await UniTask.Delay(TimeSpan.FromSeconds(Data.Cooldown));
        _isOnCooldown = false;
    }
}
