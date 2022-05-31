using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
    private FirebaseAuth auth; // 파이어베이스 인증 기능 객체

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
            messageUI.text = "이메일은 8자 이상으로 구성되어야 합니다.";
            return false;
        }
        else if(password.Length < 8)
        {
            messageUI.text = "비밀번호는 8자 이상으로 구성되어야 합니다.";
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
                    messageUI.text = "이미 사용 중이거나 형식이 바르지 않습니다.";
                }
            }
         );
    }

    public void Back()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
