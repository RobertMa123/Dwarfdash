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

    private CharacterController character;
    public cinemachineControl camControl;
    public Animator animator;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
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
                    source.PlayOneShot(attackClip);
                    if (collider.gameObject.GetComponent<enemyBehavior>() != null)
                    {
                        Debug.Log("hit");
                        source.PlayOneShot(attackClipHit);
                        Destroy(collider.gameObject);
                        character.boostOnKill();
                        camControl.lockAtPosition();
                        animator.SetBool("Attack", true);
                        StartCoroutine(waitTillEndOfAnimation());

                    }
                }
            }
            animator.SetBool("Attack", false);
        }
    }

    private IEnumerator waitTillEndOfAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
    }
}
