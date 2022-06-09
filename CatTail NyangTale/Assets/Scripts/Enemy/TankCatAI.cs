using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankCatAI : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackDelayTime;
    public float waitTime;
    public GameObject canonballPrefab;
    public GameObject canonHead;
    public LayerMask playerLayer;
    
    [SerializeField] private Transform[] targetPoint;
    [SerializeField] private GameObject attackStartPoint;

    private int index = 0;
    private float curAttackTime = 0f;
    private float currentDistfromcharacter;
    private bool canMove;
    private bool canAttack;

    private Transform moveTarget;
    private GameObject attackTarget;
    private NavMeshAgent nav;
    private Rigidbody rigid;

    private Animator anim;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveTarget = targetPoint[0];
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void Update()
    {
        curAttackTime += Time.deltaTime;

        AttackTargeting();

        if (canMove)
        {
            nav.SetDestination(moveTarget.position);
            TargetPositionCheck();
        }
        else
        {
            StartCoroutine(WaitForIt());
        }

        if(curAttackTime >= attackDelayTime){
            Attack();
        }
    }

    void AttackTargeting(){
        Collider[] hit = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

        if (hit.Length == 0)
        {
            //canonHead.transform.rotation = Quaternion.Euler(0,0,0);
            return;
        }

        currentDistfromcharacter = attackRange * 100;

        // 범위 안의 적들 중 가장 가까운 적 찾기
        foreach (Collider character in hit)
        {
            Vector3 dist = character.transform.position - transform.position;
            float sqrLen = dist.sqrMagnitude;

            if (sqrLen < currentDistfromcharacter)
            {
                currentDistfromcharacter = sqrLen;
                attackTarget = character.gameObject;
            }

            canonHead.transform.LookAt(attackTarget.transform);
        }
    }

    void TargetPositionCheck()
    {
        //Debug.Log((target.position - gameObject.transform.position).sqrMagnitude);
        if ((moveTarget.position - gameObject.transform.position).sqrMagnitude < 1f && canMove)
        {
            canMove = false;
            index++;
            if (index > targetPoint.Length)
            {
                index = 0;
            }
            moveTarget = targetPoint[index];
            //Debug.Log("Target: " + target.gameObject.name);
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }

    void Attack(){
        curAttackTime = 0f;
        Instantiate(canonballPrefab, attackStartPoint.transform.position, attackStartPoint.transform.rotation);
        Debug.Log("발사");
        OnAttack();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
    }
}
