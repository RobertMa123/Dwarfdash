using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Move Speed")]
    public float moveSpeed;

    [Header("Orientation of Player")]
    public Transform orientation;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        LookAtMouse();
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg), Vector3.forward);
    }

    private void MovePlayer()
    {
        rb.AddRelativeForce(new Vector3(moveSpeed, 0.0f, 0.0f));
    }
}
