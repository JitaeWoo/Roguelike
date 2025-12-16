using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerRuntimeData _runtimeData;
    private AfterimageController _afterimageController;

    [Inject]
    private void Init(PlayerRuntimeData data, AfterimageController afterimageController)
    {
        _runtimeData = data;
        _afterimageController = afterimageController;
    }

    private void Start()
    {
        _runtimeData.IsDash.Subscribe(OnDash)
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
}
