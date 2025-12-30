using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HpPresenter : BaseUI
{
    private Slider _hpBar;

    private PlayerManager _playerManager;

    [Inject]
    private void Init(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    protected override void Awake()
    {
        base.Awake();

        _hpBar = GetUI<Slider>("HpBar");
    }

    private void Start()
    {
        _playerManager.Data.Hp
            .Subscribe(hp => _hpBar.value = hp)
            .AddTo(this);
    }
}
