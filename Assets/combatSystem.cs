using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatSystem : MonoBehaviour
{
    [SerializeField]
    private float attackArea;
    [SerializeField]
    private AudioClip attackClip;
    [SerializeField]
    private AudioClip attackClipHit;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip inAttackRange;
    private AudioSource attackSource;
    public Animator animator;

    private CharacterController character;
    public cinemachineControl camControl;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
        float RunAnimSpeed = 1 - character.currentSpeedPos;
        if (RunAnimSpeed > 0)
        {
            animator.SetFloat("runSpeed", RunAnimSpeed);
        }
        else RunAnimSpeed = 0;

        
    }
    private void attack()
    {
        //When attacking, check the overlap circle and then
        Collider2D[] colliderHit = Physics2D.OverlapCircleAll(transform.position, attackArea);

        foreach (Collider2D collider in colliderHit)
        {
            if (collider != null)
            {
                {
                    animator.SetBool("Attack", true);
                    source.PlayOneShot(attackClip);
                    if (collider.gameObject.tag == "enemy")
                    {
                        
                        Debug.Log("hit");
                        source.PlayOneShot(attackClipHit);
                        Destroy(collider.gameObject);
                        character.boostOnKill();
                        camControl.lockAtPosition();
                    }
                }
            }
        }
        StartCoroutine(ResetHit());
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Attack", false);
    }

}
