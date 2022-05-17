using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public HealthBar healthBar;

    private int currentHealth;
    private Rigidbody rigid;

    void Start()
    {
        currentHealth = maxHealth;
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.UpdateHealth((float)currentHealth / (float)maxHealth);
        Debug.Log(this.gameObject.name + "'s health: " + currentHealth);

        // 공격 당하는 애니메이션 실행

        if(currentHealth <= 0){
            Die();
        }
    }

    public void knockBack(GameObject knockBackFrom, float knockBackForce)
    {
        Vector3 dir = (gameObject.transform.position - knockBackFrom.transform.position).normalized; // 캐릭터와 적간의 거리를 구해서 캐릭터가 에너미로 향하는 방향을 구함
        rigid.AddForce(dir * knockBackForce, ForceMode.Impulse);
    }

    public void Die()
    {
        // 죽는 애니메이션 실행
        GameObject.Destroy(this.gameObject);
        if(this.gameObject.CompareTag("Player"))
        GameManager.instance.OnPlayerDead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("tree"))
        {
            Die();
        }
        /*     else if (collision.collider.CompareTag("Friends"))
             {
                 Die();
             }
        */

    }

}
