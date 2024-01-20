using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public GameObject particlesObject;

    private void OnDestroy()
    {
        Instantiate(particlesObject, transform.position, Quaternion.identity);
    }
}
