using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int countdownMinutes = 3;
    [SerializeField] Text countdownText;
    private float countdownSeconds;
    public GameObject rangeObject;
    public GameObject Player;
    public GameObject Enemy;
    public int enemySpwanTimeMin;
    public int enemySpwanTimeMax;
    public float friendSpwanTime;
    BoxCollider rangeCollider;
    bool bossTime;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        StopAllCoroutines();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
       
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = UnityEngine.Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 1f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> friend = new List<GameObject>();
    int spawnEnemy;
    int enemyIndex;
    int friendIndex;
    Scene scene;
   

    IEnumerator EnemyRandomRespawn_Coroutine()
    {
        while (countdownSeconds > 0 && bossTime == false)
        {
            spawnEnemy = UnityEngine.Random.Range(enemySpwanTimeMin, enemySpwanTimeMax);
            enemyIndex = UnityEngine.Random.Range(0, enemy.Count);
            yield return new WaitForSeconds(spawnEnemy);
            GameObject instantEnemy = Instantiate(enemy[enemyIndex], Return_RandomPosition(), Quaternion.identity);
            instantEnemy.transform.parent = Enemy.transform;
        }
    }

    IEnumerator FriendsRandomRespawn_Coroutine()
    {
        while (countdownSeconds > 0 && bossTime == false)
        {
            friendIndex = UnityEngine.Random.Range(0, friend.Count);
            yield return new WaitForSeconds(friendSpwanTime);
            GameObject instantFriends = Instantiate(friend[friendIndex], Return_RandomPosition(), Quaternion.identity);
        }
    }

    void Start()
    {
        bossTime = false;
        countdownSeconds = countdownMinutes * 60;
        GameObject instantPlayer = Instantiate(Player, new Vector3(0, -1, 0), Quaternion.identity);
        StartCoroutine(EnemyRandomRespawn_Coroutine());
        StartCoroutine(FriendsRandomRespawn_Coroutine());
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        countdownText.text = span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0 && bossTime == false)
        {
            bossTime = true;
            StopAllCoroutines();
            countdownSeconds += 60;
            Destroy(Enemy);
        }

        if (countdownSeconds <= 0 && bossTime == true)
        {
            if(scene.name == "MainGameScene")
            SceneManager.LoadScene("stage2");
            else if (scene.name == "stage2")
            SceneManager.LoadScene("stage3");
            else if (scene.name == "stage3")
            SceneManager.LoadScene("EndScene");
        }
    }

    public void isBossDead()
    {
        if (scene.name == "MainGameScene")
            SceneManager.LoadScene("stage2");
        else if (scene.name == "stage2")
            SceneManager.LoadScene("stage3");
        else if (scene.name == "stage3")
            SceneManager.LoadScene("EndScene");
    }
}
