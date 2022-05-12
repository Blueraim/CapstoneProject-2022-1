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

   

    void Start()
    {
        Destroy(gameObject, 10f);
    }
   
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position , target.transform.position, speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Instantiate(explodeZone, target.transform.position, gameObject.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
