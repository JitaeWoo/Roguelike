using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] private float _hpSetting;

    public readonly ReactiveProperty<float> Hp = new ReactiveProperty<float>();

    private SpriteRenderer _renderer;
    private Collider2D _collider;

    [Inject]
    private void Init(SpriteRenderer renderer, Collider2D collider)
    {
        _renderer = renderer;
        _collider = collider;
    }


    private void Awake()
    {
        Hp.Value = _hpSetting;
    }

    private void Start()
    {
        Hp.Where(hp => hp <= 0)
            .Subscribe(v => Die())
            .AddTo(this);
    }

    public void TakeDamage(float amount)
    {
        if (Hp.Value <= 0) return;

        Hp.Value -= amount;
    }

    private void Die()
    {
        _collider.enabled = false;

        _renderer.material.DOFloat(0, "_SplitValue", 1f).OnComplete(() => Destroy(gameObject));
    }
}
