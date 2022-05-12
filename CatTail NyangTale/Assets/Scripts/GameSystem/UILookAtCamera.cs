using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
