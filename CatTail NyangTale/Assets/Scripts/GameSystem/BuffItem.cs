using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Friends"){
            Destroy(gameObject);
        }
    }
}
