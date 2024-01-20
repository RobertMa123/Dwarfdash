using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Options")]
    public float speed;
    public float multiplier;
    public float slowMultiplier;
    public float topSpeed;

    [Header("OnKill")]
    public float speedBoostOnKill;
    public float speedImmediateBoost;

    [Header("Speed Curve")]
    public float speedCurveChange;
   
    public float currentSpeedPos;

    public AnimationCurve speedCurve;

    public float baseDelayMultiplier;

    public Transform player;

    [Header("Sounds")]
    public AudioClip[] wallHitSounds;
    public AudioClip[] footstepClips;
    public AudioSource source;
    public AudioSource footStepSource;

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

        if (speed > topSpeed / 1.3f) footStepSource.clip = footstepClips[0];
        else if (speed > topSpeed / 2f) footStepSource.clip = footstepClips[1];
        else if (speed > topSpeed / 3f) footStepSource.clip = footstepClips[2];
        else if (speed > topSpeed / 5f) footStepSource.clip = footstepClips[3];
        else if (speed > topSpeed / 8f) footStepSource.clip = footstepClips[4];
        else if (speed > topSpeed / 13f) footStepSource.clip = footstepClips[5];
        footStepSource.PlayOneShot();
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

        speed = speedCurve.Evaluate(currentSpeedPos) * topSpeed;
          
        float delayMultiplier = baseDelayMultiplier * Vector2.Distance(player.transform.position, Input.mousePosition);

        float reverseDistance = baseDelayMultiplier - Vector2.Distance(Input.mousePosition, playerRigid.position);
        float fixedDistance = Mathf.Clamp(reverseDistance, 0f, 4f);
        float delay = baseDelayMultiplier + fixedDistance;


        Vector2 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 wantedDirection = mousePos.normalized * speed;
        Vector2 currentDirection = Vector2.Lerp(playerRigid.velocity, wantedDirection, Time.deltaTime);

        playerRigid.velocity = currentDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "enemy")
        {
            dead = true;
            particle.Play();
        }
        source.PlayOneShot(wallHitSounds[UnityEngine.Random.Range(1, wallHitSounds.Length)]);
    }
}
