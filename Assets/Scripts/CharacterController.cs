using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Options")]
    public float speed;
    public float slowMultiplier;
    public float topSpeed;

    [Header("OnKill")]
    public float speedBoostOnKill;
    public float speedImmediateBoost;

    [Header("Speed Curve")]
    public float speedCurveChange;
    [HideInInspector]
    public float currentSpeedPos;

    [SerializeField]
    private float multiplierOfStaticness;

    public AnimationCurve speedCurve;

    public Transform player;

    [Header("Wall Hit Sounds")]
    public AudioClip[] wallHitSounds;
    public AudioSource source;

    [Header("Easy Mode?")]
    public bool easyMode;

    private Rigidbody2D playerRigid;

    [SerializeField]
    private ParticleSystem particle;

    public bool dead = false;



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

    public void boostOnKill()
    {
        //TODO: FIX WITH OVER TIME (COUROTINE??)
        currentSpeedPos -= speedBoostOnKill;
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 speedBoost = direction.normalized * speedImmediateBoost;
        playerRigid.AddForce(new Vector2 (speedImmediateBoost, 0));
    }

    private void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);


        transform.rotation = Quaternion.AngleAxis((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg), Vector3.forward);
    }

    private void MovePlayer()
    {
        //Clamp currentSpeedPos between 0 and 1 where 0 is max speed and 1 is nothing
        Mathf.Clamp(currentSpeedPos, 0, 1);

        if (easyMode)    //Cap speed at some topSpeed on easy mode
        {
            if(speed >= topSpeed)
            {
                speed = topSpeed;
            }
        }
        currentSpeedPos += Time.deltaTime * slowMultiplier;

        if (dead == true)
        {
            currentSpeedPos = 1;
        }
        speed = speedCurve.Evaluate(currentSpeedPos) * topSpeed;

        if (speed <= 1)
        {
            onDeath();
        }



        Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 wantedDirection = mousePos.normalized * speed;
        Vector2 currentDirection = Vector2.Lerp(playerRigid.velocity, wantedDirection, Time.deltaTime * multiplierOfStaticness);

        playerRigid.velocity = currentDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            onDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "enemy")
        {
            source.PlayOneShot(wallHitSounds[UnityEngine.Random.Range(1, wallHitSounds.Length)]);
        }
    }

    private void onDeath()
    {
        dead = true;
        particle.Play();
    }
}
