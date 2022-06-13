using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdelClass : MonoBehaviour
{
    public string className;
    public float attackRate;                // ���� �ӵ�
    public float attackRange;               // ���� �����Ÿ�
    public int damage;                      // ������

    public Transform attackStartPoint;      // ���� ���� ��ġ
    public GameObject attackTarget;         // ���� ���� ��ġ
    public GameObject swordPrefab;

    public Animator anim;

    public ParticleSystem muzzleFlash;
    public AudioClip attack_Sound;
    public LayerMask enemyLayer;

    public void DamageBuff(){
        damage += 10;

        Invoke("BuffOff", 10f);
    }

    void BuffOff(){
        damage -= 10;
    }
}
