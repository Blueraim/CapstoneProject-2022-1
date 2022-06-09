using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComputerCatAI : MonoBehaviour
{
    public float detectRange;
    public int damage;
    public bool isChase;
    public bool isAttack;
    public LayerMask playerLayer;
    public Transform target;

    private NavMeshAgent nav;
    private Rigidbody rigid;

    private Animator anim;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled && target != null)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    void ChaseStart()
    {
        isChase = true;
    }

    void Targeting()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, detectRange, playerLayer);
        if (hit.Length != 0 && !isAttack)
        {
            foreach (Collider player in hit)
            {
                target = player.transform;
            }
            nav.SetDestination(target.position);
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        OnAttack();// 애니메이션 실행

        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector3.zero;

        yield return new WaitForSeconds(2f);

        isChase = true;
        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (isAttack && (other.gameObject.tag == "Player" || other.gameObject.tag == "Friends")) // 동료들 태그도 추가해야 함
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
    }
}
