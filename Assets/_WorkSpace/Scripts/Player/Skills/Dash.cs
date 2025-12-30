using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    [SerializeField] private DashData _data;

    private void Start()
    {
        SetData(_data);
    }

    protected override void ActivateSkill()
    {
    }
}
