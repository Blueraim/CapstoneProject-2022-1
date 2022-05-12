using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour
{
    public GameObject prefab;

    public void objectOnOff(){
        if(prefab.activeSelf){
            prefab.SetActive(false);
        }
        else{
            prefab.SetActive(true);
        }
    }

    public void SceneRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
