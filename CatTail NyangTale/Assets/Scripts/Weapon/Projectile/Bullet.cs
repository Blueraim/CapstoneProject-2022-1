using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Invoke("BulletDestroy", 6.0f);
        }
        else if (other.gameObject.layer == 3)
        {
            Invoke("BulletDestroy", 6.0f);
        }
    }
    public void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
