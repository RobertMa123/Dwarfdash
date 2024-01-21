using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDeathWall : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = Vector3.up * speed;
        speed += Time.deltaTime;
    }
}
