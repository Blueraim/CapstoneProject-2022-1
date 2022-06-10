using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("UIBlink", 7f);
        Invoke("BuffOff", 10f);
    }

    void UIBlink(){
        anim.SetTrigger("UIBlink");
    }

    void BuffOff(){
        gameObject.SetActive(false);
    }
}
