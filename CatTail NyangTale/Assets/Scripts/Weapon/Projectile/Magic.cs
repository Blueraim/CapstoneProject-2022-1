using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]
    private WizardClass _wizard;
    [SerializeField]
    private float speed;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 6)
        { 
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
