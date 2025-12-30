using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterPresenter : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    private MonsterData _data;

    [Inject]
    private void Init(Animator animator, SpriteRenderer renderer, MonsterData data)
    {
        _animator = animator;
        _renderer = renderer;
        _data = data;
    }

    private void Start()
    {
        _data.IsChase
            .Subscribe(v => OnChase(v))
            .AddTo(this);

        _data.IsDead
            .Subscribe(v => OnDead(v))
            .AddTo(this);
    }

    private void OnChase(bool value)
    {
        _animator.SetBool("IsChase", value);
    }

    private void OnDead(bool value)
    {
        if (!value) return;

        _animator.SetBool("IsDead", value);

        _renderer.material
            .DOFloat(0, "_SplitValue", 1f).OnComplete(() => Object.Destroy(gameObject))
            .SetLink(gameObject);
    }
}
