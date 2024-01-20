using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Options")]
    public float speed;
    public float multiplier;
    public float speedUpMultiplier;
    public float topSpeed;

    public float baseDelayMultiplier;

    public Transform player;

    [Header("Easy Mode?")]
    public bool easyMode;

    private Rigidbody2D playerRigid;


    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        LookAtMouse();
        MovePlayer();
    }

    void FixedUpdate()
    {
    }

    private void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);


        transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg), Vector3.forward);
    }

    private void MovePlayer()
    {
        
        if (easyMode)    //Cap speed at some topSpeed on easy mode
        {
            if(speed >= topSpeed)
            {
                speed = topSpeed;
            }
        }
        speed += Time.deltaTime * speedUpMultiplier;
          //Increase player speed over time
        float delayMultiplier = baseDelayMultiplier * Vector2.Distance(player.transform.position, Input.mousePosition);

        float reverseDistance = baseDelayMultiplier - Vector2.Distance(Input.mousePosition, playerRigid.position);
        float fixedDistance = Mathf.Clamp(reverseDistance, 0f, 4f);
        float delay = baseDelayMultiplier + fixedDistance;


        Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 wantedDirection = mousePos.normalized * speed;
        Vector2 currentDirection = Vector2.Lerp(playerRigid.velocity, wantedDirection, Time.deltaTime * delay);

        playerRigid.velocity = currentDirection;
    }
}
