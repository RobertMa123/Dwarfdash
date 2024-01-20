using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Options")]
    public float speed;
    public float multiplier;

    public float baseDelayMultiplier;

    public Transform player;

    private Rigidbody2D playerRigid;


    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        LookAtMouse();

        Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 wantedDirection = mousePos.normalized * speed;
        Vector2 currentDirection = Vector2.Lerp(playerRigid.velocity, wantedDirection, Time.deltaTime * baseDelayMultiplier);

        playerRigid.velocity = currentDirection;

    }
    
    void FixedUpdate()
    {
        //MovePlayer();
    }

    private void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        
        transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg), Vector3.forward);
    }
}
