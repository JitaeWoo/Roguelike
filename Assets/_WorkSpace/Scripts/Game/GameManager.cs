using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    private int _curStage = 1;
    public int CurStage => _curStage;

    private PlayerManager _playerManager;

    [Inject]
    private void Init(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public void StageClear()
    {
        if(_curStage == 3)
        {
            GameClear();
            return;
        }

        _curStage++;
        SceneManager.LoadScene("Stage");
    }

    private void GameClear()
    {
        SceneManager.LoadScene("Clear");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        _playerManager.Data.Hp.Value = 100;
    }
}
