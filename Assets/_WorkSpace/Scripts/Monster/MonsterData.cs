using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    public float Damage = 20f;
    public ReactiveProperty<float> Hp = new ReactiveProperty<float>(20f);
    public float MoveSpeed = 5f;

    public ReactiveProperty<bool> IsChase = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>();
}
