using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    [SerializeField] float _hp = 20f;

    public float Damage = 20f;
    public ReactiveProperty<float> Hp = new ReactiveProperty<float>();
    public float MoveSpeed = 5f;

    public AudioData DeadSfx;

    public ReactiveProperty<bool> IsChase = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>();

    private void Awake()
    {
        Hp.Value = _hp;
    }
}
