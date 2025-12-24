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
        // TODO : 클리어 화면으로
        Debug.Log("GameClear");
    }
}
