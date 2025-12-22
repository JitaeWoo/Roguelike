using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public enum SkillTypes
{
    Shot, Size
}

public class SkillManager : MonoBehaviour
{
    [SerializeField] GameObject[] _skillList;
    [SerializeField] public Dash Dash { get; private set; }

    private Dictionary<SkillTypes, Skill>[] _skillDict;
    private SkillTypes[] _curType;


    private void Awake()
    {
        if(Dash == null)
        {
            Dash = transform.GetChild(0).gameObject.GetComponent<Dash>();
        }

        _skillDict = new Dictionary<SkillTypes, Skill>[transform.childCount - 1];
        _curType = new SkillTypes[transform.childCount - 1];

        for(int i = 1; i < transform.childCount; i++)
        {
            _skillDict[i - 1] = new Dictionary<SkillTypes, Skill>();

            Transform skill = transform.GetChild(i);
            for (int j = 0; j < _skillList.Length; j++)
            {
                GameObject child = Instantiate(_skillList[j], skill);
                child.name = _skillList[j].name;
                if (Enum.TryParse(child.name, out SkillTypes type))
                {
                    _skillDict[i - 1].Add(type, child.GetComponent<Skill>());

                    child.SetActive(false);

                    _curType[i - 1] = SkillTypes.Size;
                }
                else
                {
                    Debug.LogError($"잘못된 이름의 스킬 오브젝트가 입니다 : {child.name}");
                }
            }
        }
    }

    public void SetSkill(SkillData data, int index)
    {
        if(data.Type != _curType[index] && _curType[index] != SkillTypes.Size)
        {
            _skillDict[index][_curType[index]].gameObject.SetActive(false);
        }

        _curType[index] = data.Type;
        _skillDict[index][data.Type].gameObject.SetActive(true);
        _skillDict[index][data.Type].SetData(data);
    }

    public void UseSkill(int index)
    {
        if (_curType[index] == SkillTypes.Size) return;

        _skillDict[index][_curType[index]].Trigger();
    }
}
