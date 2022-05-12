using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFriend : MonoBehaviour
{

    public GameObject rangeObject;
    BoxCollider rangeCollider;
    // Start is called before the first frame update

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        
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

    public List<GameObject> capsul = new List<GameObject>();
    int i = 0;

    private void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while (i < capsul.Count)
        {
            yield return new WaitForSeconds(30f);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(capsul[i], Return_RandomPosition(), Quaternion.identity);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
