using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerDamageHandler : MonoBehaviour, IDamagable
{
    private PlayerManager _playerManager;
    private GameManager _gameManager;

    [Inject]
    private void Init(PlayerManager playerManager, GameManager gameManager)
    {
        _playerManager = playerManager;
        _gameManager = gameManager;
    }

    public void TakeDamage(float amount)
    {
        if (_playerManager.Data.Hp.Value <= 0) return;

        _playerManager.Data.Hp.Value -= amount;


        if (_playerManager.Data.Hp.Value > 0) return;

        Die();
    }

    private void Die()
    {
        _gameManager.GameOver();
    }
}
