using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using System;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using Zenject;

public class CooldownPresenter : BaseUI
{
    private Slider _dashCooldown;
    private Slider _skill1Cooldown;

    private SkillManager _skillManager;

    [Inject]
    private void Init(SkillManager skillMangaer)
    {
        _skillManager = skillMangaer;
    }

    protected override void Awake()
    {
        base.Awake();
        _dashCooldown = GetUI<Slider>("DashCool");
        _skill1Cooldown = GetUI<Slider>("Skill1Cool");
    }

    private void Start()
    {
        _skillManager.Dash.Event
            .Subscribe(unit =>
            {
                _dashCooldown.value = 1;
                _dashCooldown.DOValue(0, _skillManager.Dash.CoolDown)
                    .SetEase(Ease.Linear)
                    .SetLink(_dashCooldown.gameObject);
                
            }).AddTo(this);


        _skillManager.GetSkillChangeEvent(0)
            .Select(_ => _skillManager.GetSkill(0))
            .Where(s => s != null)
            .Do(_ =>
            {
                if (_skill1Cooldown != null)
                {
                    _skill1Cooldown.DOKill();
                    _skill1Cooldown.value = 0;
                }
            })
            .Select(skill =>
            {
                return skill.Event.AsObservable();
            })
            .Switch()
            .Subscribe(unit =>
            {
                _skill1Cooldown.value = 1;
                _skill1Cooldown.DOValue(0, _skillManager.GetSkill(0).CoolDown)
                    .SetEase(Ease.Linear)
                    .SetLink(_skill1Cooldown.gameObject);
            })
            .AddTo(this);
    }
}
