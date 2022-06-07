using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UFOCatAI : MonoBehaviour
{
    public float walkPointRange;
    public float nextWalkTime;
    private bool walkPointSet = false;
    public Vector3 walkPoint;

    public float attackDelayTime;
    public GameObject laserPrefab;
    public Transform attackStartPoint;
    public AudioClip clip;

    private float curAttackTime = 0f;
    private float curWalkTime = 0f;
    private bool canAttack;
    private Rigidbody rigid;
    private NavMeshAgent nav;
    private Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        curAttackTime += Time.deltaTime;
        curWalkTime += Time.deltaTime;

        if (curAttackTime >= attackDelayTime)
        {
            Attack();
            WeaponSoundManager.instance.SFXPlay("UFO", clip);
        }
        
        if(curWalkTime >= nextWalkTime){
            SearchWalkPoint();
            nav.SetDestination(walkPoint);
        }

        Patroling();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void Attack(){
        curAttackTime = 0f;
        Instantiate(laserPrefab, attackStartPoint.position, attackStartPoint.rotation);
        OnAttack();
    }

    void Patroling(){
        if(!walkPointSet) SearchWalkPoint();
        else nav.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Debug.Log(distanceToWalkPoint.magnitude);
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
            
    }

    void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        curWalkTime = 0;
        walkPointSet = true;
    }

    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
    }
}
