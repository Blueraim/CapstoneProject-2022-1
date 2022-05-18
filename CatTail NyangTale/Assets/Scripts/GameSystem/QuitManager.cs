using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{

    uint exitCountValue = 0;

    // Update is called once per frame
    void Update()
    {
        //뒤로가기 버튼 2번 누르면 프로그램 종료
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            exitCountValue++;
            if (IsInvoking("disable_DoubleClick"))
                Invoke("disable_DoubleClick", 0.3f);
        }
        if (exitCountValue == 2)
        {
            CancelInvoke("disable_DoubleClick");
            Application.Quit();
        }
    }

    void disable_DoubleClick()
    {
        exitCountValue = 0;
    }

}
