using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCyborgAI : MonoBehaviour
{
    private float curWalkTime = 0f;
    private NavMeshAgent agent;
    private Rigidbody rigid;
    private Transform player;

    public float nextWalkTime;
    public LayerMask playerLayer;

    // 순찰
    public float detectPosition;
    public float walkPointRange;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool canMove = true;
    
    // 공격
    public int damage;
    public float timeBetweenAttacks;
    public GameObject attackPosition;
    private bool alreadyAttacked;

    // 상태
    public float sightRange, attackRange;
    private bool playerInSightRange, playerIsAttackRange;

    private void Awake()
    {
        player = GameObject.Find("cat_player_bamkong(Clone)").transform;
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void Update()
    {
        curWalkTime += Time.deltaTime;

        playerInSightRange = Physics.CheckSphere(transform.position + transform.forward * detectPosition, sightRange, playerLayer);
        playerIsAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInSightRange && !playerIsAttackRange){
            Patroling();

            if(curWalkTime >= nextWalkTime){
                SearchWalkPoint();
                agent.SetDestination(walkPoint);
            }
        }
        if(playerInSightRange && !playerIsAttackRange && canMove) ChasePlayer();
        if(playerInSightRange && playerIsAttackRange) AttackPlayer();


    }

    void Patroling(){
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

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

    void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    void AttackPlayer(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            Attack();

            alreadyAttacked = true;
            canMove = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else{
            StartCoroutine(Wait());
        }
    }

    void ResetAttack(){
        alreadyAttacked = false;
    }

    void Attack(){
        Collider[] hit = Physics.OverlapSphere(attackPosition.transform.position, attackRange, playerLayer);

        if (hit.Length != 0)
        {
            foreach (Collider player in hit)
            {
                player.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
        
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(timeBetweenAttacks);
        canMove = true;
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * detectPosition, sightRange);
    }
}
