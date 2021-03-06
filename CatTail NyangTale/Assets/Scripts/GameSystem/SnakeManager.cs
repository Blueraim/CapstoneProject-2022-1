using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public float BodySpeed;
    public int Gap;
    private int Warrior = 0;
    private int Archer = 1;
    private int Wizard = 2;
    private int Cowboy = 3;
    private int Bomber = 4;
    private int IceWizard = 5;
    private int Adel = 6;
    private int StarWizard = 7;
    private int Gunner = 8;
    private int friends;
    private GameObject attackBuffUI;
    [SerializeField] List<GameObject> prefab = new List<GameObject>();

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();

    void Awake()
    {
        attackBuffUI = GameObject.Find("AttackBuffUI");
        attackBuffUI.SetActive(false);
    }

    void Update()
    {
        PositionHistory.Insert(0, transform.position);
        
        int index = 1;
        foreach (var body in BodyParts)
        {
            if (body != null)
            {
                Vector3 point = PositionHistory[Mathf.Clamp(index * Gap, 0, PositionHistory.Count - 1)];
                Vector3 moveDirection = point - body.transform.position;
                body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
                body.transform.LookAt(point);
                index++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("1"))
        {
            GrowSnake(Warrior);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("2"))
        {
            GrowSnake(Archer);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("3"))
        {
            GrowSnake(Wizard);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("4"))
        {
            GrowSnake(Cowboy);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("5"))
        {
            GrowSnake(Bomber);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("6"))
        {
            GrowSnake(IceWizard);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("7"))
        {
            GrowSnake(Adel);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("8"))
        {
            GrowSnake(StarWizard);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("9"))
        {
            GrowSnake(Gunner);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("DamageBuff")){
            DamageBuff();
            attackBuffUI.SetActive(true);
        }
        else if (other.CompareTag("HealthBuff")){
            HealthBuff();
        }
    }

    private void GrowSnake(int i)
    {
        GameObject body = Instantiate(prefab[i]);
        body.transform.parent = GameManager.instance.PlayerParent.transform;
        BodyParts.Add(body);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("tree"))
        {
            Destroy(gameObject);
            GameManager.instance.OnPlayerDead();
        }
        
    }

    private void DamageBuff(){
        gameObject.GetComponent<WarriorClass>().DamageBuff();

        foreach (var body in BodyParts)
        {
            if (body != null)
            {
                body.SendMessage("DamageBuff", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void HealthBuff(){
        gameObject.GetComponent<Health>().HealthBuff();

        foreach (var body in BodyParts)
        {
            if (body != null)
            {
                body.SendMessage("HealthBuff", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
