using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private MeshRenderer render;

    public float speed;
    private float offset;

    void Start()
    {
        Time.timeScale = 1;
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, offset);
    }
}
