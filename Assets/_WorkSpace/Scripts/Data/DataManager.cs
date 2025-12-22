using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<SkillData> _skillList = new List<SkillData>();

    public Dictionary<string, SkillData> SkillDatas = new Dictionary<string, SkillData>();

    private void Awake()
    {
        foreach(SkillData data in _skillList)
        {
            SkillDatas.Add(data.Name, data);
        }
    }
}
