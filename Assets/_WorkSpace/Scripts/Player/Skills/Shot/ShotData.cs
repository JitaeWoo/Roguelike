using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObjects/ShotData")]
public class ShotData : SkillData
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _damage;
    [SerializeField] private float _shotSpeed;

    public Sprite Sprite => _sprite;
    public float Damage => _damage;
    public float ShotSpeed => _shotSpeed;

    private void Awake()
    {
        Type = SkillTypes.Shot;
    }
}
