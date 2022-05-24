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
    private Vector3 enemy;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            enemy = target.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, enemy, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        /*Debug.Log(other.gameObject.layer);*/
        if (other.gameObject.layer == 6)
        { 
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
        }
    }
}
