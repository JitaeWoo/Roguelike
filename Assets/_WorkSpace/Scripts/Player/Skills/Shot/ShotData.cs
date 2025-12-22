using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObjects/ShotData")]
public class ShotData : SkillData
{
    public Sprite Sprite;
    public float Damage;
    public float ShotSpeed;
    public float CoolDown;

    private void Awake()
    {
        Type = SkillTypes.Shot;
    }
}
