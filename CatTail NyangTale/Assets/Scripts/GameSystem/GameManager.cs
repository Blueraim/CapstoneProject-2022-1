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
    public Text gameScoreText;
    private float gameScore = 0;
    private float countdownSeconds;
    public GameObject rangeObject;
    public GameObject Player;
    public GameObject Enemy;
    public int enemySpwanTimeMin;
    public int enemySpwanTimeMax;
    public float friendSpwanTime;
    BoxCollider rangeCollider;
    private bool bossTime;
    public GameObject gameOver;
    public static GameManager instance;
    private bool isGameover = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            {
                Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다.");
                Destroy(gameObject); //자기 자신을 삭제
            }
        }
        Application.targetFrameRate = 60;
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        StopAllCoroutines();
        gameOver.SetActive(false);
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
        isGameover = false;
        countdownSeconds = countdownMinutes * 60;
        GameObject instantPlayer = Instantiate(Player, new Vector3(0, -1, 0), Quaternion.identity);
        StartCoroutine(EnemyRandomRespawn_Coroutine());
        StartCoroutine(FriendsRandomRespawn_Coroutine());
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        timer();

    }

    public void timer()
    {
        if (isGameover != true)
        {
            gameScore += Time.deltaTime;
            countdownSeconds -= Time.deltaTime;
        }
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        countdownText.text = span.ToString(@"mm\:ss");
             
           if (countdownSeconds <= 0 && bossTime == false)
        {
            bossTime = true;
            StopAllCoroutines();
            countdownSeconds += 60;
            findAllChildren(Enemy);
        }

        if (countdownSeconds <= 0 && bossTime == true)
        {
            if (scene.name == "MainGameScene")
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

    public void findAllChildren(GameObject Enemy)
    {
        Transform[] enemyChild = Enemy.GetComponentsInChildren<Transform>();
        if(enemyChild != null)
        {
            for(int i =1; i< enemyChild.Length; i++)
            {
                if (enemyChild[i] != transform)
                    Destroy(enemyChild[i].gameObject);
            }
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        gameOver.SetActive(true);
        ScoreAdd();
    }


    public void ReTry()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ScoreAdd()
    {
        var Score = new TimeSpan(0, 0, (int)gameScore);
        gameScoreText.text = "생존 시간 : " + Score.ToString(@"mm\:ss");
    }

}
