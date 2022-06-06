using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAutoAttack : MonoBehaviour
{
    [SerializeField]
    private WarriorClass warrior;

    private float currentAttackRate;

    public LayerMask enemyLayer;

    public AudioClip clip;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        currentAttackRate = warrior.attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        AttackRateCal();

        if (currentAttackRate <= 0)
        {
            Attack();
            WeaponSoundManager.instance.SFXPlay("Warrior", clip);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void AttackRateCal()
    {
        if (currentAttackRate > 0)
        {
            currentAttackRate -= Time.deltaTime;
        }
    }

    void Attack()
    {
        
        WarriorAttack();
        OnAttack(); // 공격 애니메이션 실행

        // 공격 딜레이 초기화
        currentAttackRate = warrior.attackRate;
    }

    void WarriorAttack()
    {
        // 사거리 안의 적 검사
        Collider[] hitEnemies = Physics.OverlapSphere(      // 충돌을 감지하는 구체를 생성
            warrior.attackPoint.position,                   // 구체 생성 위치(공격 위치)
            warrior.attackRange,                            // 구체 크기(공격 범위)
            enemyLayer                                      // 적을 레이어로 판단
        );                                                  // 충돌한 물체 중 적으로 판단된 것들을 hitEnemies에 저장

        // 적에게 데미지 주기
        foreach (Collider enemy in hitEnemies)
        {
            if(warrior.className == "기본 전사")
            {
                enemy.GetComponent<Health>().TakeDamage(warrior.damage);
            }
            else if(warrior.className == "카우보이")
            {
                //넉백
                enemy.GetComponent<Health>().knockBack(gameObject, warrior.knockBackForce);
                /*Debug.Log("넉백");*/

                enemy.GetComponent<Health>().TakeDamage(warrior.damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(warrior.attackPoint.position, warrior.attackRange);
    }

    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
    }


}
