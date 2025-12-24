using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;
using R3;

public class Tests : MonoBehaviour
{
    [SerializeField] InputAction _input1;
    [SerializeField] InputAction _input2;
    [SerializeField] InputAction _input3;

    private PlayerManager _playerManager;

    [Inject]
    private void Init(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    private void OnEnable()
    {
        _input1.Enable();
        _input2.Enable();
        _input3.Enable();

        _input1.performed += _ => SetSkill(1);
        _input2.performed += _ => SetSkill(2);
        _input3.performed += _ => SetSkill(3);
    }

    private void SetSkill(int index)
    {
        _playerManager.Data.Skill1.Value = $"Shot{index}";
    }
}
