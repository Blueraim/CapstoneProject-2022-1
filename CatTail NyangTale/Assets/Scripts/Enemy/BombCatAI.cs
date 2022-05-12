using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombCatAI : MonoBehaviour
{
    public float detectRange;
    public LayerMask playerLayer;
    public GameObject explosion;

    private Transform target;
    private NavMeshAgent nav;

    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
        target = null;
    }

    private void Update() {
        TargetDetect();
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
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Explosion();
        }
    }

    void Explosion(){
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
