using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T에는 각 상태를 표현할 Enum 클래스를 넣어주면 됩니다.
public class StateMachine<T> where T : Enum
{
    private BaseState<T> _curState;
    public T CurStateEnum;
    private Dictionary<T, BaseState<T>> _stateDict = new Dictionary<T, BaseState<T>>();

    public void ChangeState(T changedStateEnum)
    {
        if (!_stateDict.ContainsKey(changedStateEnum))
        {
            Debug.LogError($"{changedStateEnum} 상태가 등록되지 않았습니다.");
            return;
        }

        BaseState<T> changedState = _stateDict[changedStateEnum];

        if (_curState == changedState) return;

        // 처음에 없을 수도 있으니 null 체크
        _curState?.Exit();
        CurStateEnum = changedStateEnum;
        _curState = changedState;
        _curState.Enter();
    }

    public void Update() => _curState.Update();

    public void FixedUpdate()
    {
        if (_curState.HasPhysics)
        {
            _curState.FixedUpdate();
        }
    }

    public void AddState(T stateEnum, BaseState<T> state)
    {
        _stateDict.TryAdd(stateEnum, state);
    }
}
