using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdelAuttoAttack : MonoBehaviour
{
    [SerializeField]
    private AdelClass adel;
    private float currentAttackRate;
    private float currentDistfromEnemy;
    private Sword _sword;
    public AudioClip clip;
    public LayerMask enemyLayer;
    private Animator anim;
    public GameObject swordPosition;

    void Start()
    {
        currentAttackRate = adel.attackRate;
        currentDistfromEnemy = adel.attackRange * 100;

        _sword = adel.swordPrefab.GetComponent<Sword>();
        _sword.damage = adel.damage;
      
    }
    void Update()
    {
        AttackRateCal();

        if (currentAttackRate <= 0)
        {
            SetTarget();
            classCheck();

            if (adel.attackTarget != null)
            {
                Attack();
            }
            else
            {

            }

            currentAttackRate = adel.attackRate;
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void classCheck()
    {
            _sword.damage = adel.damage;
            _sword.target = adel.attackTarget;
    }

    void AttackRateCal()
    {
        if (currentAttackRate > 0)
        {
            currentAttackRate -= Time.deltaTime;
        }
    }

    void SetTarget()
    {
        // 사거리 안의 적 검사
        Collider[] hitEnemies = Physics.OverlapSphere(      // 충돌을 감지하는 구체를 생성
            adel.attackStartPoint.position,               // 구체 생성 위치(공격 위치)
            adel.attackRange,                             // 구체 크기(공격 범위)
            enemyLayer                                      // 적을 레이어로 판단
        );                                                  // 충돌한 물체 중 적으로 판단된 것들을 hitEnemies에 저장

        if (hitEnemies.Length == 0)
        {
            adel.attackTarget = null;
            //Debug.Log("타겟: 없음");
            return;
        }

        currentDistfromEnemy = adel.attackRange * 100;

        // 범위 안의 적들 중 가장 가까운 적 찾기
        foreach (Collider enemy in hitEnemies)
        {
            Vector3 dist = enemy.transform.position - adel.transform.position;
            float sqrLen = dist.sqrMagnitude;

            if (sqrLen < currentDistfromEnemy)
            {
                currentDistfromEnemy = sqrLen;
                adel.attackTarget = enemy.gameObject;
            }
        }

        //Debug.Log("타겟: " + wizard.attackTarget.gameObject.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(adel.attackStartPoint.position, adel.attackRange);
    }

    void Attack()
    {
        AdelAttack();
        currentAttackRate = adel.attackRate;
    }

    void AdelAttack()
    { 
        Instantiate(adel.swordPrefab, swordPosition.transform.position, swordPosition.transform.rotation);
    }
}
