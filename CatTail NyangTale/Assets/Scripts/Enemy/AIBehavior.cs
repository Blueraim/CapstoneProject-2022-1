using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    [SerializeField]
    private float detectRange;
    [SerializeField]
    private float colliderForwardPos;
    private float shortestDist;
    private float f_angleBtwWithWall;
    private Vector3 angleBtwWithWall;
    private Vector3 colliderPos;
    private Vector3 nearestPos;
    private Vector3 forward, ySide, normalVec;

    public LayerMask WallLayer;
    
    private void Start() {
        ySide = new Vector3(0, 1, 0); 
    }

    private void Update() {
        forward = gameObject.transform.forward;

        normalVectorCal();
        WallCheck();
        VectorFromWallToPlayer();
        
    }

    void normalVectorCal(){
        normalVec = Vector3.Cross(ySide , forward);
        Debug.DrawRay(colliderPos, normalVec);
    }

    void WallCheck(){
        colliderPos = transform.position + (transform.forward * colliderForwardPos); 
        Collider[] hitWalls = Physics.OverlapSphere(colliderPos, detectRange, WallLayer);

        if (hitWalls.Length == 0)
        {
            shortestDist = 500;
            nearestPos = new Vector3(0, 0, 0);
            return;
        }

        for (int i = 0; i < hitWalls.Length; i++)
        {
            Vector3 dist = transform.position - hitWalls[i].transform.position;
            float sqrLen = dist.sqrMagnitude;

            if (sqrLen < shortestDist)
            {
                shortestDist = sqrLen;
                nearestPos = hitWalls[i].transform.position;
            }
        }
        //Debug.Log(nearestPos);
    }

    void VectorFromWallToPlayer(){
        if(nearestPos != new Vector3(0, 0, 0)){
            angleBtwWithWall = (colliderPos - nearestPos).normalized;
            angleBtwWithWall.y = 0;
            angleBtwWithWall *= detectRange;

            //Debug.Log(angleBtwWithWall);
            Debug.DrawRay(colliderPos, angleBtwWithWall);

            f_angleBtwWithWall = GetAngle(normalVec, angleBtwWithWall);
            Debug.Log(f_angleBtwWithWall);
            // 구한 각도로 회전?
        }
    }

    float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v2 = end - start;
        return Mathf.Atan2(v2.z, v2.x) * Mathf.Rad2Deg;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(colliderPos, detectRange);
    }
}
