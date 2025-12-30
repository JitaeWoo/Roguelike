using R3;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class MonsterCountPresenter : BaseUI
{
    private TextMeshProUGUI _countText;

    private StageManager _stageManager;

    [Inject]
    private void Init(StageManager stageManager)
    {
        _stageManager = stageManager;
    }

    protected override void Awake()
    {
        base.Awake();
        _countText = GetUI<TextMeshProUGUI>("CountText");
    }

    private void Start()
    {
        _stageManager.MonsterCount
            .Subscribe(count => _countText.text = $"Monster : {count}")
            .AddTo(this);
    }
}
