using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Text userUI;

    void Start()
    {
        userUI.text = PlayerInformation.auth.CurrentUser.Email + " ��, ȯ���մϴ�.";
            
    }
    public void LogOut()
    {
        PlayerInformation.auth.SignOut();
        SceneManager.LoadScene("LoginScene");
    }
}
