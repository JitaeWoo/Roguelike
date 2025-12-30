using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float MoveSpeed = 3f;
    public float DashSpeed = 10f;
    public float Damage = 5f;

    public ReactiveProperty<float> Hp = new ReactiveProperty<float>(100);

    public ReactiveProperty<string> Skill1 = new ReactiveProperty<string>();
}
