using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public SkillTypes Type { get; protected set; }
    public string Name;
    public float Cooldown;
}
