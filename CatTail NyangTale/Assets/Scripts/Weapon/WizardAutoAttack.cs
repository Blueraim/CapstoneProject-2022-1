using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAutoAttack : MonoBehaviour
{
    [SerializeField]
    private WizardClass wizard;

    private float currentAttackRate;
    private float currentDistfromEnemy;
    private Magic _magic;
    private FireMagic _fireMagic;
    private StarMagic _starMagic;
    private explosion explosion;

    public AudioClip clip;

    public LayerMask enemyLayer;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAttackRate = wizard.attackRate;
        currentDistfromEnemy = wizard.attackRange * 100;

        if (wizard.className == "기본 마법사")
        {
            _magic = wizard.magicPrefab.GetComponent<Magic>();
            _magic.damage = wizard.damage;
        }
        else if (wizard.className == "얼음 마법사")
        {
            _fireMagic = wizard.magicPrefab.GetComponent<FireMagic>();
            explosion = _fireMagic.explodeZone.GetComponent<explosion>();
            _fireMagic.damage = wizard.damage;
        }
        else if(wizard.className == "별 마법사"){
            _starMagic = wizard.magicPrefab.GetComponent<StarMagic>();
            _starMagic.star[0].damage = wizard.damage;
            _starMagic.star[1].damage = wizard.damage;
        }
    }

    void Update()
    {
        AttackRateCal();
        
        if (currentAttackRate <= 0)
        {
            SetTarget();
            classCheck();
            
            if(wizard.attackTarget != null){
                Attack();
            }
            else if(wizard.className == "별 마법사"){
                Attack();
            }

            currentAttackRate = wizard.attackRate;
        }
    }

    void classCheck()
    {
        if (wizard.className == "기본 마법사")
        {
            _magic.damage = wizard.damage;
            _magic.target = wizard.attackTarget;
        }
        else if (wizard.className == "얼음 마법사")
        {
            explosion.damage = wizard.damage;
            _fireMagic.target = wizard.attackTarget;
        }
        else if(wizard.className == "별 마법사"){
            _starMagic.star[0].damage = wizard.damage;
            _starMagic.star[1].damage = wizard.damage;
        }
    }

    void AttackRateCal()
    {
        if (currentAttackRate > 0)
        {
            currentAttackRate -= Time.deltaTime;
        }
    }

    void SetTarget(){
        // 사거리 안의 적 검사
        Collider[] hitEnemies = Physics.OverlapSphere(      // 충돌을 감지하는 구체를 생성
            wizard.attackStartPoint.position,               // 구체 생성 위치(공격 위치)
            wizard.attackRange,                             // 구체 크기(공격 범위)
            enemyLayer                                      // 적을 레이어로 판단
        );                                                  // 충돌한 물체 중 적으로 판단된 것들을 hitEnemies에 저장

        if(hitEnemies.Length == 0){
            wizard.attackTarget = null;
            //Debug.Log("타겟: 없음");
            return;
        }
        
        currentDistfromEnemy = wizard.attackRange * 100;

        // 범위 안의 적들 중 가장 가까운 적 찾기
        foreach (Collider enemy in hitEnemies)
        {
            Vector3 dist = enemy.transform.position - wizard.transform.position;
            float sqrLen = dist.sqrMagnitude;
    
            if(sqrLen < currentDistfromEnemy){
                currentDistfromEnemy = sqrLen;
                wizard.attackTarget = enemy.gameObject;
            }
        }

        //Debug.Log("타겟: " + wizard.attackTarget.gameObject.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wizard.attackStartPoint.position, wizard.attackRange);
    }

    void Attack()
    {
        WizardAttack();
        OnAttack(); // 공격 애니메이션 실행
        // 공격 딜레이 초기화
        currentAttackRate = wizard.attackRate;
    }

    void WizardAttack(){
        // 공격 발사

        if(wizard.className == "얼음 마법사"){
            Instantiate(wizard.magicPrefab, gameObject.transform.position + new Vector3(0,6,0), wizard.transform.rotation);
            return;
        }
        else if(wizard.className == "별 마법사"){
            wizard.magicPrefab.SetActive(true);
            Invoke("MagicOff", wizard.attackRate - 5f);
        }
        else
            Instantiate(wizard.magicPrefab, gameObject.transform.position, wizard.transform.rotation);
    }

    void MagicOff(){
        wizard.magicPrefab.SetActive(false);
    }

    public void OnAttack()
    {
        anim.SetTrigger("onAttack");
        //WeaponSoundManager.instance.SFXPlay("Wizard", clip);
    }
}

