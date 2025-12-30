using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class Skill : MonoBehaviour
{
    public SkillData Data { get; private set; }

    public float CoolDown => Data.Cooldown;
    private bool _isOnCooldown;
    public bool CanUse = true;
    private Subject<Unit> _event = new Subject<Unit>();
    public Subject<Unit> Event => _event;

    private CancellationTokenSource _source;

    protected PlayerManager Player;
    protected DiContainer DiContainer;
    protected AudioManager Audio;

    [Inject]
    private void Init(PlayerManager playerManager, DiContainer di, AudioManager audio)
    {
        Player = playerManager;
        DiContainer = di;
        Audio = audio;
    }

    private void OnEnable()
    {
        if (_source != null)
        {
            _source.Dispose();
        }

        _source = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _source.Cancel();
        _source.Dispose();
        _source = null;
    }

    public void SetData(SkillData data)
    {
        Data = data;
        _source.Cancel();
        _source.Dispose();
        _source = new CancellationTokenSource();
        _isOnCooldown = false;
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
        await UniTask.Delay(TimeSpan.FromSeconds(Data.Cooldown), cancellationToken: _source.Token);
        _isOnCooldown = false;
    }
}
