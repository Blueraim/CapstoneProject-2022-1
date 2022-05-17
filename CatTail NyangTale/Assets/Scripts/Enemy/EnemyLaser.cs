using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public int damage;
    public float speed;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Friends")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        if(other.tag != "Enemy") 
            Destroy(gameObject);
    }

    private void Update() {
        rigid.velocity = gameObject.transform.up * speed;
    }
}
