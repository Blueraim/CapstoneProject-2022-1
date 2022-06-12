using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private AdelClass _adel;
    [SerializeField]
    private float speed = 10.0f;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public int damage;
    private Vector3 enemy;

    public GameObject swordParticle;

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
            transform.LookAt(enemy);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, enemy, speed * Time.deltaTime);
        }
    }
    private void OnDestroy()
    {
        if (target != null)
            Instantiate(swordParticle, target.transform.position+ new Vector3(0,3,0), gameObject.transform.rotation);
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
