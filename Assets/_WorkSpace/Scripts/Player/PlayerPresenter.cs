using R3;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
    private const string MOVE_STRING = "IsMove";

    private PlayerRuntimeData _runtimeData;
    private AfterimageController _afterimageController;
    private Animator _animator;

    [Inject]
    private void Init(PlayerRuntimeData data, AfterimageController afterimageController, Animator animator)
    {
        _runtimeData = data;
        _afterimageController = afterimageController;
        _animator = animator;
    }

    private void Start()
    {
        _runtimeData.IsDash.Subscribe(OnDash)
            .AddTo(this);

        _runtimeData.IsMove.Subscribe(OnMove)
            .AddTo(this);
    }

    private void OnDash(bool value)
    {
        if(value)
        {
            _afterimageController.Play();
        }
        else
        {
            _afterimageController.Stop();
        }
    }

    private void OnMove(bool value)
    {
        _animator.SetBool(MOVE_STRING, value);
    }
}
