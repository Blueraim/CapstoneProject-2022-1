using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // 동료들 태그도 추가해야 함
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
