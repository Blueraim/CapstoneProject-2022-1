using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParticle : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        Destroy(gameObject, 0.75f);
    }

}
