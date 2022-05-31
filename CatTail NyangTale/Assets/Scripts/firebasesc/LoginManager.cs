using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    private FirebaseAuth auth; // ���̾�̽� ���� ��� ��ü

    public InputField emailInputField;
    public InputField passwordInputField;

    public Text messageUI;


    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    public void Login()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if(task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    PlayerInformation.auth = auth;
                    SceneManager.LoadScene("StartScene");
                }
                else
                {
                    messageUI.text = "������ �ٽ� Ȯ�����ּ���.";
                }
            }
            );
    }
    public void GoToJoin()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
