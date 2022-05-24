using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartScene : MonoBehaviour
{
    public void ToStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGameScene");
    }

    public void Main()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}