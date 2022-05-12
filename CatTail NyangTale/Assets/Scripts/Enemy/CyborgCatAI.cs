using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyborgCatAI : MonoBehaviour
{
    //public float detectRange;
    public float attackRange;
    public int damage;
    public bool isChase;
    public bool isAttack;
    public LayerMask playerLayer;
    public Transform target;
    public Transform attackPoint;

    private NavMeshAgent nav;
    private Rigidbody rigid;
    //private Animator animator;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        target = FindObjectOfType<MovementObject>().transform;
        //animator = GetComponent<Animator>();

        Invoke("ChaseStart", 2);
    }

    private void FixedUpdate() {
        Targeting();
        FreezeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if(nav.enabled){
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }   
    }

    void ChaseStart(){
        isChase = true;
    }

    void Targeting(){
        Collider[] hit = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        if (hit.Length != 0 && !isAttack)
        {
            foreach (Collider player in hit)
            {
                StartCoroutine(Attack(player));
            }
        }
    }

    IEnumerator Attack(Collider _player){
        isChase = false;
        isAttack = true;
        // 애니메이션 실행
        //animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        _player.gameObject.GetComponent<Health>().TakeDamage(damage);

        yield return new WaitForSeconds(2f);

        isChase = true;
        isAttack = false;
        // 애니메이션 종료
        //animator.SetBool("isAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void FreezeVelocity(){
        if(isChase){
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
}
