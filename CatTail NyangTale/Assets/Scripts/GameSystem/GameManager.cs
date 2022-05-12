using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float setTime = 180.0f;
    [SerializeField] Text countdownText;
    public GameObject rangeObject;
    public int enemySpwanTimeMin;
    public int enemySpwanTimeMax;
    public float friendSpwanTime;
    BoxCollider rangeCollider;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        StopAllCoroutines();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 1f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> friend = new List<GameObject>();
    int spawnEnemy;
    int enemyIndex;
    int friendIndex = 0;
    Scene scene;
   

    IEnumerator EnemyRandomRespawn_Coroutine()
    {
        while (setTime > 0)
        {
            spawnEnemy = Random.Range(enemySpwanTimeMin, enemySpwanTimeMax);
            enemyIndex = Random.Range(0, enemy.Count);
            yield return new WaitForSeconds(spawnEnemy);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(enemy[enemyIndex], Return_RandomPosition(), Quaternion.identity);
           
        }
    }

    IEnumerator FriendsRandomRespawn_Coroutine()
    {
        while (friendIndex < friend.Count)
        {
            yield return new WaitForSeconds(friendSpwanTime);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(friend[friendIndex], Return_RandomPosition(), Quaternion.identity);
            friendIndex++;
        }
    }

    void Start()
    {
        countdownText.text = setTime.ToString();
        StartCoroutine(EnemyRandomRespawn_Coroutine());
        StartCoroutine(FriendsRandomRespawn_Coroutine());
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0)
            setTime -= Time.deltaTime;
        else if (setTime <= 0)
        {
            //Time.timeScale = 0.0f;
            if(scene.name == "MainGameScene")
            SceneManager.LoadScene("stage2");
            if (scene.name == "stage2")
            SceneManager.LoadScene("stage3");
            if (scene.name == "stage3")
            SceneManager.LoadScene("EndScene");
        }
        countdownText.text = Mathf.Round(setTime).ToString();
    }


}
