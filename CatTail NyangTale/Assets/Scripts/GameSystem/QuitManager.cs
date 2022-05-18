using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{

    uint exitCountValue = 0;

    // Update is called once per frame
    void Update()
    {
        //�ڷΰ��� ��ư 2�� ������ ���α׷� ����
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
