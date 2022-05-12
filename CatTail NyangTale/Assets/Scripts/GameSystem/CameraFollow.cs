using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Camera;

    private void Start() {
        Camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Camera.transform.position = new Vector3(transform.position.x, transform.position.y + 15f, transform.position.z - 13f);
    }
}
