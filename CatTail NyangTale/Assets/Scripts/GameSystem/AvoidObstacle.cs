using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacle : MonoBehaviour
{
    // Start is called before the first frame update

    private int collisionTime;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("tree"))
        {
            collisionTime += 1;
            if(collisionTime >= 3)
            {
                gameObject.transform.position = GameManager.instance.Return_RandomPosition();
            }
        }
    }
}
