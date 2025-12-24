using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public SkillTypes Type { get; protected set; }

    [SerializeField] private string _name;
    [SerializeField] private float _cooldown;

    public string Name => _name;
    public float Cooldown => _cooldown;
}
