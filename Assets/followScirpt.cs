using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScirpt : MonoBehaviour
{
    public GameObject playerObject;

    private void Update()
    {
        transform.position = playerObject.transform.position;
        transform.rotation = playerObject.transform.rotation;
    }
}
