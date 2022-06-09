using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : MonoBehaviour
{
    public string className;                // 클래스 이름
    public float health;                    // 체력
    public float attackRange;               // 사정거리
    public float attackRate;                // 공격 속도
    public float knockBackForce;            // 넉백 힘
    public int damage;                      // 데미지

    public Transform attackPoint;           // 공격 위치

    public Animator anim;
    
    public ParticleSystem muzzleFlash;
    public AudioClip attack_Sound;

    public void DamageBuff(){
        damage += 10;

        Invoke("BuffOff", 10f);
    }

    void BuffOff(){
        damage -= 10;
    }
}
