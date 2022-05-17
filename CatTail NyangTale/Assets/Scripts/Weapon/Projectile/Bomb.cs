using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private ArcherClass _archer;
    [SerializeField]
    private float speed;
    private float bezierValue;

    public float explodeDelay;
    public GameObject explodeZone;
    [HideInInspector]
    public Vector3 target;

    private Vector3[] beziers = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, explodeDelay);
        bezierValue = 0;
        beziers[0] = gameObject.transform.position;
        beziers[1] = gameObject.transform.position + new Vector3(0, 5, 0);
        beziers[2] = target + new Vector3(0, 5, 0);
        beziers[3] = target + new Vector3(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 곡사 구현
        if(bezierValue < 1){
            bezierValue += Time.deltaTime;
        }
        
        gameObject.transform.position = Bezier(beziers[0], beziers[1], beziers[2], beziers[3], bezierValue);
    }

    private void OnDestroy() {
        //폭발 이펙트
      /*  Debug.Log("폭탄 폭발");*/

        Instantiate(explodeZone, gameObject.transform.position, gameObject.transform.rotation);
    }

    Vector3 Bezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float Value){

        Vector3 A = Vector3.Lerp(p1, p2, Value);
        Vector3 B = Vector3.Lerp(p2, p3, Value);
        Vector3 C = Vector3.Lerp(p3, p4, Value);

        Vector3 D = Vector3.Lerp(A, B, Value);
        Vector3 E = Vector3.Lerp(B, C, Value);

        Vector3 F = Vector3.Lerp(D, E, Value);

        return F;
    }
}
