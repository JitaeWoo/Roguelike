using Cysharp.Threading.Tasks.CompilerServices;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
    public Subject<Unit>[] _skillChangeEvent;

    private DiContainer _diContainer;

    [Inject]
    private void Init(PlayerManager playerManager, DataManager dataManager, DiContainer di)
    {
        playerManager.Data.Skill1
            .Where(str => !string.IsNullOrEmpty(str))
            .Subscribe(str => SetSkill(dataManager.SkillDatas[str], 0))
            .AddTo(this);
        _diContainer = di;
    }

    private void Awake()
    {
        if (Dash == null)
        {
            Dash = transform.GetChild(0).gameObject.GetComponent<Dash>();
        }

        int count = transform.childCount - 1;

        _skillDict = new Dictionary<SkillTypes, Skill>[count];
        _curType = new SkillTypes[count];
        _skillChangeEvent = new Subject<Unit>[count];

        for (int i = 0; i < count; i++)
        {
            _skillDict[i] = new Dictionary<SkillTypes, Skill>();
            _skillChangeEvent[i] = new Subject<Unit>();

            Transform skill = transform.GetChild(i + 1);
            for (int j = 0; j < _skillList.Length; j++)
            {
                GameObject child = _diContainer.InstantiatePrefab(_skillList[j], skill);
                child.name = _skillList[j].name;
                if (Enum.TryParse(child.name, out SkillTypes type))
                {
                    _skillDict[i].Add(type, child.GetComponent<Skill>());

                    child.SetActive(false);

                    _curType[i] = SkillTypes.Size;
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
        if (data.Type != _curType[index] && _curType[index] != SkillTypes.Size)
        {
            _skillDict[index][_curType[index]].gameObject.SetActive(false);
        }

        _curType[index] = data.Type;
        _skillDict[index][data.Type].gameObject.SetActive(true);
        _skillDict[index][data.Type].SetData(data);
        _skillChangeEvent[index].OnNext(Unit.Default);
    }

    public void UseSkill(int index)
    {
        if (_curType[index] == SkillTypes.Size) return;

        _skillDict[index][_curType[index]].Trigger();
    }

    public Skill GetSkill(int index)
    {
        if (_curType[index] == SkillTypes.Size) return null;

        return _skillDict[index][_curType[index]];
    }

    public Subject<Unit> GetSkillChangeEvent(int index)
    {
        return _skillChangeEvent[index];
    }
}
