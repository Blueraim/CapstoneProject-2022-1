using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ResumeBtn;
    public GameObject MainBtn;
    bool pauseActive = false;

    public void pauseBtn()
    {
        Time.timeScale = 0;
        pauseActive = true;
        Panel.SetActive(true);
        ResumeBtn.SetActive(true);
        MainBtn.SetActive(true);
    }
    public void isPauseBtn()
    {
        Time.timeScale = 1;
        pauseActive = false;
        Panel.SetActive(false);
        ResumeBtn.SetActive(false);
        MainBtn.SetActive(false);
    }
}
