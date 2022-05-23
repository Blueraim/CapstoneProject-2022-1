using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAutoAttack : MonoBehaviour
{
    [SerializeField]
    private ArcherClass archer;

    private float currentAttackRate;
    private float currentDistfromEnemy;

    private Arrow _arrow;
    private Bomb _bomb;
    private explosion explosion;

    public AudioClip clip;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackRate = archer.attackRate;
        currentDistfromEnemy = archer.attackRange;

        if(archer.className == "기본 궁수"){
            _arrow = archer.arrowPrefab.GetComponent<Arrow>();
            //Debug.Log("화살 컴포넌트");
        }
        else if(archer.className == "폭탄병"){
            _bomb = archer.arrowPrefab.GetComponent<Bomb>();
            explosion = _bomb.explodeZone.GetComponent<explosion>();
            //Debug.Log("폭탄 컴포넌트");
        }
    }

    void Update()
    {
        AttackRateCal();
        
        if (currentAttackRate <= 0)
        {
            classCheck();

            Attack();

            WeaponSoundManager.instance.SFXPlay("Archer", clip);
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

    void classCheck(){
        if (archer.className == "기본 궁수")
        {
            _arrow.damage = archer.damage;
        }
        else if (archer.className == "폭탄병")
        {
            explosion.damage = archer.damage;
            SetRandomTarget();
            _bomb.target = archer.attackTarget.transform.position;
        }
        else if(archer.className != "특수 궁수"){
            _arrow.damage = archer.damage;
            SetTarget();
        }
    }

    void Attack()
    {
        OnAttack();

        ArcherAttack();

        // 공격 딜레이 초기화
        currentAttackRate = archer.attackRate;
    }

    void ArcherAttack()
    {
        // 공격 발사
        Instantiate(archer.arrowPrefab, archer.attackStartPoint.position, archer.transform.rotation);
    }

    void SetTarget()
    {
        // 사거리 안의 적 검사
        Collider[] hitEnemies = Physics.OverlapSphere(      // 충돌을 감지하는 구체를 생성
            archer.attackStartPoint.position,               // 구체 생성 위치(공격 위치)
            archer.attackRange,                             // 구체 크기(공격 범위)
            archer.enemyLayer                               // 적을 레이어로 판단
        );                                                  // 충돌한 물체 중 적으로 판단된 것들을 hitEnemies에 저장

        if (hitEnemies.Length == 0)
        {
            archer.attackTarget = null;
            //Debug.Log("타겟: 없음");
            return;
        }

        currentDistfromEnemy = archer.attackRange;

        // 범위 안의 적들 중 가장 가까운 적 찾기
        foreach (Collider enemy in hitEnemies)
        {
            Vector3 dist = enemy.transform.position - archer.transform.position;
            float sqrLen = dist.sqrMagnitude;

            if (sqrLen < currentDistfromEnemy)
            {
                currentDistfromEnemy = sqrLen;
                archer.attackTarget = enemy.gameObject;
            }
        }

        //Debug.Log("타겟: " + wizard.attackTarget.gameObject.name);
    }

    void SetRandomTarget(){
        archer.attackTarget.gameObject.transform.localPosition = new Vector3(
            Random.Range(-archer.attackRange, archer.attackRange),
            0.25f,
            Random.Range(-archer.attackRange, archer.attackRange)
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(archer.attackStartPoint.position, archer.attackRange);
    }

    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
    }
}
