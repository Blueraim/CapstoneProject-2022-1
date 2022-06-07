using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombCatAI : MonoBehaviour
{
    public float detectRange;
    public LayerMask playerLayer;
    public GameObject explosion;

    private bool targetOff = true;
    private Transform target;
    private NavMeshAgent nav;
    private Animator anim;

    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
        target = null;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if(targetOff)
            TargetDetect();

        if(target != null){
            nav.SetDestination(target.position);
        }
    }

    void TargetDetect()
    {
        // 사거리 안의 플레이어 검사
        Collider[] hit = Physics.OverlapSphere(transform.position, detectRange, playerLayer);

        if (hit.Length != 0)
        {
            foreach (Collider player in hit)
            {
                target = player.gameObject.transform;
                nav.SetDestination(target.position);
                anim.SetTrigger("onMove");
            }
            targetOff = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Friends"){
            Explosion();
        }
    }

    void Explosion(){
        Instantiate(explosion, transform.position + new Vector3(0,0.2f,0), transform.rotation);
        Destroy(gameObject);
    }

    public void OnMove()
    {
        anim.SetTrigger("onMove");
    }
}
