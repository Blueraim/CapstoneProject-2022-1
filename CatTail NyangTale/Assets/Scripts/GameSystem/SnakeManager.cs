using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public float BodySpeed;
    public int Gap;

    [SerializeField] List<GameObject> prefab = new List<GameObject>();

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();

    void Start()
    {
       
    }

    void Update()
    {
        PositionHistory.Insert(0, transform.position);
        
        int index = 1;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Clamp(index * Gap ,0, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < prefab.Count; i++)
        {
            // ####tag 말고 friend 오브젝트에 스크립트 추가해서 그 안의 변수로 구분?####
            if(other.CompareTag((i + 1).ToString())){
                Destroy(other.gameObject);
                GrowSnake(i);

                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("tree"))
        {
            Destroy(this.gameObject);
        }
    }

    private void GrowSnake(int i)
    {
        GameObject body = Instantiate(prefab[i]);
        BodyParts.Add(body);
    }
}
