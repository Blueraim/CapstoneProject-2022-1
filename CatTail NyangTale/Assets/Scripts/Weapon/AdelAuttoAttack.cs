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
        // ��Ÿ� ���� �� �˻�
        Collider[] hitEnemies = Physics.OverlapSphere(      // �浹�� �����ϴ� ��ü�� ����
            adel.attackStartPoint.position,               // ��ü ���� ��ġ(���� ��ġ)
            adel.attackRange,                             // ��ü ũ��(���� ����)
            enemyLayer                                      // ���� ���̾�� �Ǵ�
        );                                                  // �浹�� ��ü �� ������ �Ǵܵ� �͵��� hitEnemies�� ����

        if (hitEnemies.Length == 0)
        {
            adel.attackTarget = null;
            //Debug.Log("Ÿ��: ����");
            return;
        }

        currentDistfromEnemy = adel.attackRange * 100;

        // ���� ���� ���� �� ���� ����� �� ã��
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

        //Debug.Log("Ÿ��: " + wizard.attackTarget.gameObject.name);
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
