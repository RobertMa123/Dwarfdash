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
    public AudioClip[] goblinDeathSound;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip inAttackRange;
    private AudioSource attackSource;

    private CharacterController character;
    public cinemachineControl camControl;
    public Animator animator;

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
        animator.SetFloat("runSpeed", 1 - character.currentSpeedPos);
    }

    private void attack()
    {
        //When attacking, check the overlap circle and then
        Collider2D[] colliderHit = Physics2D.OverlapCircleAll(transform.position, attackArea);
        animator.SetBool("Attack", true);
        foreach (Collider2D collider in colliderHit)
        {
            if (collider != null)
            {
                {
                    source.PlayOneShot(attackClip);
                    if (collider.gameObject.GetComponent<enemyBehavior>() != null)
                    {
                        Debug.Log("hit");
                        source.PlayOneShot(attackClipHit);
                        source.PlayOneShot(goblinDeathSound[Random.Range(0,goblinDeathSound.Length)]);
                        Destroy(collider.gameObject);
                        character.boostOnKill();
                        //camControl.lockAtPosition();
                    }
                }
            }
           
        }
        StartCoroutine(waitTillEndOfAnimation());
    }

    private IEnumerator waitTillEndOfAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Attack", false);
    }
}
