using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public HealthBar healthBar;

    private int currentHealth;
    private Rigidbody rigid;
    public bool isAlive = true;
    private Animator anim;

    private void Awake()
    {
        currentHealth = maxHealth;
        rigid = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isAlive == true)
        {
            currentHealth -= damage;
            healthBar.UpdateHealth((float)currentHealth / (float)maxHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
            isAlive = false;
        }
    }

    public void knockBack(GameObject knockBackFrom, float knockBackForce)
    {
        Vector3 dir = (gameObject.transform.position - knockBackFrom.transform.position).normalized; // 캐릭터와 적간의 거리를 구해서 캐릭터가 에너미로 향하는 방향을 구함
        rigid.AddForce(dir * knockBackForce, ForceMode.Impulse);
    }

    public void Die()
    {
        switch (this.gameObject.tag)
        {
            case "Player":
                GameManager.instance.OnPlayerDead();
                break;
            case "Friends":
                break;
            case "Enemy":
                break;
            case "Boss":
                GameManager.instance.SendMessage("BossDead", SendMessageOptions.DontRequireReceiver);
                break;
        }
        OnDead();
        Invoke("GameObjectDestroy", 1.5f);
    }

    void GameObjectDestroy(){
        GameObject.Destroy(this.gameObject);
    }

    public void OnDead()
    {
        anim.SetTrigger("onDead");
    }

    public float GetCurrentHealth(){
        return currentHealth;
    }

    public void HealthBuff(){
       /* Debug.Log("BuffTest");*/
        if(currentHealth + 10 > maxHealth){
            currentHealth = maxHealth;
        }
        else
            currentHealth += 10;

        healthBar.UpdateHealth((float)currentHealth / (float)maxHealth);
    }
}
