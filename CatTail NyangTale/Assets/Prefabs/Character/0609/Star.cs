using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int damage;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
