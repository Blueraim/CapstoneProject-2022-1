using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardClass : MonoBehaviour
{
    public string className;
    public float health;
    public float attackRate;                // 공격 속도
    public float attackRange;               // 공격 사정거리
    public int damage;                      // 데미지

    public Transform attackStartPoint;      // 공격 시작 위치
    public GameObject attackTarget;         // 공격 시작 위치
    public GameObject magicPrefab;

    public Animator anim;

    public ParticleSystem muzzleFlash;
    public AudioClip attack_Sound;
    public LayerMask enemyLayer;

    void DamageBuff(){
        damage += 10;

        Invoke("BuffOff", 10f);
    }

    void BuffOff(){
        damage -= 10;
    }
}
