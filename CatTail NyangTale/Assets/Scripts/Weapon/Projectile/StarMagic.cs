using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMagic : MonoBehaviour
{
    public Star[] star;
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
