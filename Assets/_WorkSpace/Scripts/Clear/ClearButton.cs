using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearButton : MonoBehaviour
{
    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
