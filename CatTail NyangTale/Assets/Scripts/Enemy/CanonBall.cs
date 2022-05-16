using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public float canonballForce;
    public GameObject explosion;
    private Rigidbody rigid;

    private void OnEnable() {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * canonballForce;
    }

    private void OnTriggerEnter(Collider other) {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
