using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    [SerializeField] private WizardClass fireWizard;
    [SerializeField] private float speed;

    public GameObject explodeZone;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public int damage;
    private Vector3 enemy;


    void Start()
    {
       
       
    }

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

    private void OnDestroy()
    {
        if(target != null)
        Instantiate(explodeZone, target.transform.position, gameObject.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        /*Debug.Log(other.gameObject.layer);*/
        if (other.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
        }
    }
}
