using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
    private FirebaseAuth auth; // ���̾�̽� ���� ��� ��ü

    public InputField emailInputField;
    public InputField passwordInputField;

    public Text messageUI;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    bool InputCheck()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        if(email.Length < 8)
        {
            messageUI.text = "�̸����� 8�� �̻����� �����Ǿ�� �մϴ�.";
            return false;
        }
        else if(password.Length < 8)
        {
            messageUI.text = "��й�ȣ�� 8�� �̻����� �����Ǿ�� �մϴ�.";
            return false;
        }
        messageUI.text = "";
        return true;
    }
    // Update is called once per frame
    public void Check()
    {
        InputCheck();
    }

    public void Join()
    {
        if(!InputCheck())
        {
            return;
        }

        string email = emailInputField.text;
        string password = passwordInputField.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if(!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log("!");
                    SceneManager.LoadScene("LoginScene");
                }
                else
                {
                    messageUI.text = "�̹� ��� ���̰ų� ������ �ٸ��� �ʽ��ϴ�.";
                }
            }
         );
    }

    public void Back()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
