using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStageClear : MonoBehaviour
{
    public float waitTime;
    float time = 0;
    bool isOn;
    private void OnEnable()
    {
        GetComponent<Text>().color = new Color(1, 1, 0, 0);
        isOn = true;
        Invoke("OnDisable", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time < 0.7f && isOn)
        {
            GetComponent<Text>().color = new Color(1, 1, 0, time / 0.7f);
        }
        else if(time >= 0.7f && isOn)
        {
            isOn = false;
            StartCoroutine(WaitForIt());
        }

        if(time < 0.7f && !isOn){
            GetComponent<Text>().color = new Color(1, 1, 0, 1 - (time / 0.7f));
        }
    }

    IEnumerator WaitForIt(){
        yield return new WaitForSeconds(waitTime);

        time = 0f;
        isOn = false;
    }

    private void OnDisable()
    {
        GetComponent<Text>().color = new Color(1, 1, 0, 0);
        time = 0;
        gameObject.SetActive(false);
    }
}
