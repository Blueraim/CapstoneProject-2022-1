using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParticle : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        Destroy(gameObject, 0.75f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
