using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text rank1UI;
    public Text rank2UI;
    public Text rank3UI;
    public Text rank4UI;
    public Text rank5UI;
    [SerializeField] int countdownMinutes;
    [SerializeField] Text countdownText;
    public Text gameScoreText;
    private float gameScore = 0;
    private float countdownSeconds;
    public GameObject rangeObject;
    public GameObject Player;
    public GameObject EnemyParent;
    public GameObject PlayerParent;
    public GameObject FriendParent;
    public GameObject BuffParent;
    public int enemySpwanTimeMin;
    public int enemySpwanTimeMax;
    public float friendSpwanTime;
    public float buffSpwanTime;
    BoxCollider rangeCollider;
    private bool isBossTime;
    public GameObject gameOver;
    public static GameManager instance { get; set; }
    private bool isGameover = false;
    public string stage = "1";
    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> friend = new List<GameObject>();
    public List<GameObject> buff = new List<GameObject>();
    int spawnEnemy;
    int enemyIndex;
    int friendIndex;
    int buffIndex;
    Scene scene;

    public GameObject bossPrefab;
    public GameObject bossSpawnText;
    public Transform bossSpawnTransfrom;
    GameObject bossInstance;
    Health bossHealth;
    bool isBossDead;

    public GameObject stageClearText;
    private bool isResult;



    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null)
        {
            Destroy(gameObject); //자기 자신을 삭제
            Debug.LogWarning("Singletone Patern Error.");
        } 
        Application.targetFrameRate = 60;
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        StopAllCoroutines();
        gameOver.SetActive(false);
        bossSpawnText.SetActive(false);
        stageClearText.SetActive(false);
    }

    DatabaseReference reference;

    void Start()
    {
 
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://capstone4-1-920f8-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        isBossTime = false;
        isBossDead = false;
        isGameover = false;
        isResult = false;
        countdownSeconds = countdownMinutes * 60;
        GameObject instantPlayer = Instantiate(Player, new Vector3(0, -1, 0), Quaternion.identity);
        StartCoroutine(EnemyRandomRespawn_Coroutine());
        StartCoroutine(FriendsRandomRespawn_Coroutine());
        StartCoroutine(BuffRandomRespawn_Coroutine());
        scene = SceneManager.GetActiveScene();
        HighScore();
    }

    // Update is called once per frame
    void Update()
    {
        timer();
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
   
    IEnumerator EnemyRandomRespawn_Coroutine()
    {
        while (countdownSeconds > 0 && isBossTime == false)
        {
            spawnEnemy = UnityEngine.Random.Range(enemySpwanTimeMin, enemySpwanTimeMax);
            enemyIndex = UnityEngine.Random.Range(0, enemy.Count);
            yield return new WaitForSeconds(spawnEnemy);
            GameObject instantEnemy = Instantiate(enemy[enemyIndex], Return_RandomPosition(), Quaternion.identity);
            instantEnemy.transform.parent = EnemyParent.transform;
        }
    }

    IEnumerator FriendsRandomRespawn_Coroutine()
    {
        while (countdownSeconds > 0 && isBossTime == false)
        {
            friendIndex = UnityEngine.Random.Range(0, friend.Count);
            yield return new WaitForSeconds(friendSpwanTime);
            GameObject instantFriends = Instantiate(friend[friendIndex], Return_RandomPosition(), Quaternion.identity);
            instantFriends.transform.parent = FriendParent.transform;
        }
    }

    IEnumerator BuffRandomRespawn_Coroutine()
    {
        while (countdownSeconds > 0 && isBossTime == false)
        {
            buffIndex = UnityEngine.Random.Range(0, buff.Count);
            yield return new WaitForSeconds(buffSpwanTime);
            GameObject instantBuff = Instantiate(buff[buffIndex], Return_RandomPosition(), buff[buffIndex].transform.rotation);
            instantBuff.transform.parent = BuffParent.transform;
        }
    }

    public void timer()
    {
        if (isGameover == false)
        {
            gameScore += Time.deltaTime;
            countdownSeconds -= Time.deltaTime;
        }
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        countdownText.text = span.ToString(@"mm\:ss");
             
        if (countdownSeconds <= 0 && !isBossTime)
        {
            SpawnBoss();
        }

        if (isBossTime && isBossDead && !isResult)
        {
            Time.timeScale = 0;
            GameResult();
            gameOver.SetActive(true);
           /* HighScore();*/
            isResult = true;
            /* stageClearText.SetActive(true);
             *//*Invoke("loadNextScene", 4f);*/
        }
    }

    void SpawnBoss(){
        StopAllCoroutines();
        findAllChildrenEnemy(EnemyParent);

        isBossTime = true;
        isBossDead = false;
        countdownSeconds += 60;
        bossSpawnText.SetActive(true);

        Instantiate(bossPrefab, bossSpawnTransfrom.position, bossSpawnTransfrom.rotation);
    }

    public void BossDead(){
       
        isBossDead = true;
        //Debug.Log("GameManager BossDead");
    }

    public void findAllChildrenEnemy(GameObject Enemy)
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

    public void findAllChildrenFriend(GameObject Friend)
    {
        Transform[] friendChild = Friend.GetComponentsInChildren<Transform>();
        if (friendChild != null)
        {
            for (int i = 1; i < friendChild.Length; i++)
            {
                if (friendChild[i] != transform)
                    Destroy(friendChild[i].gameObject);
            }
        }
    }

    public void OnPlayerDead()
    {
        Time.timeScale = 0;
        isGameover = true;
        gameOver.SetActive(true);
        ScoreAdd();
    }

    public void ReTry()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    public void ScoreAdd()
    {
        var Score = new TimeSpan(0, 0, (int)gameScore);
        gameScoreText.text = "Servive Time : " + Score.ToString(@"mm\:ss");
    }
    
    void loadNextScene(){
        findAllChildrenEnemy(EnemyParent);
        findAllChildrenFriend(FriendParent);
    }

    void GameResult()
    {
        isGameover = true;
        ScoreAdd();
        PlayerInformation.score = (int)GameManager.instance.gameScore;
        AddRank(PlayerInformation.auth.CurrentUser.Email, (int)PlayerInformation.score);
       
    }
    void AddRank(string email, int score)
    {
        Rank rank = new Rank(email, score);
        string json = JsonUtility.ToJson(rank);
  /*      string Addscore = gameScore.ToString();*/
        reference.Child("ranks").Child(GameManager.instance.stage).Child(PlayerInformation.auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    public class Rank
    {
        public string email;
        public int score;
        public Rank(string email, int score)
        {
            this.email = email;
            this.score = score;
        }
    }
     public void NextStage()
    {
        if (stage == "1")
            SceneManager.LoadScene("stage2");
        else if (stage == "2")
            SceneManager.LoadScene("stage3");
        else if (stage == "3")
            SceneManager.LoadScene("EndScene");
        Time.timeScale = 1;
    }

    public void SaveBestScore()
    {
        if (float.Parse(GameManager.instance.gameScoreText.text) <= PlayerInformation.bestScore)
        {
            PlayerInformation.bestScore = float.Parse(GameManager.instance.gameScoreText.text);
        }
    }

    public void HighScore()
    {
        rank1UI.text = "데이터를 불러오는 중입니다.";
        rank2UI.text = "데이터를 불러오는 중입니다.";
        rank3UI.text = "데이터를 불러오는 중입니다.";
        rank4UI.text = "데이터를 불러오는 중입니다.";
        rank5UI.text = "데이터를 불러오는 중입니다.";
        DatabaseReference reference;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri("https://capstone4-1-920f8-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.GetReference("ranks").Child(GameManager.instance.stage);
        reference.OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                List<string> rankList = new List<string>();
                List<string> emailList = new List<string>();
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    emailList.Add(rank["email"].ToString());
                    rankList.Add(rank["score"].ToString());
                }
                emailList.Sort();
                rankList.Sort();
                rank1UI.text = "플레이 한 사용자가 없습니다.";
                rank2UI.text = "플레이 한 사용자가 없습니다.";
                rank3UI.text = "플레이 한 사용자가 없습니다.";
                rank4UI.text = "플레이 한 사용자가 없습니다.";
                rank5UI.text = "플레이 한 사용자가 없습니다.";
                List<Text> textList = new List<Text>();
                textList.Add(rank1UI);
                textList.Add(rank2UI);
                textList.Add(rank3UI);
                textList.Add(rank4UI);
                textList.Add(rank5UI);
                int count = 1;
                for (int i = 0; i < rankList.Count && i < 5; i++)
                {
                    textList[i].text = count + "위: " + emailList[i] + " (" + rankList[i] + " 초)";
                    count = count + 1;
                }
            }
        });
    }
}
