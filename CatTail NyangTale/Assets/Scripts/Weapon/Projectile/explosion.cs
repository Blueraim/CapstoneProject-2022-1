using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public int damage;

    private void Start() {
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
