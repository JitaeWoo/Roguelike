using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] private float _hpSetting;

    public readonly ReactiveProperty<float> Hp = new ReactiveProperty<float>();

    private void Awake()
    {
        Hp.Value = _hpSetting;
    }

    private void Start()
    {
        Hp.Where(hp => hp <= 0)
            .Skip(1)
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
        Destroy(gameObject);
    }
}
