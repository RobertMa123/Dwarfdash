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
    private AudioClip inAttackRange;
    private AudioSource attackSource;

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
        Collider2D colliderHit = Physics2D.OverlapCircle(transform.position, attackArea);
        if (colliderHit != null)
        {
            if (colliderHit.gameObject.tag == "Enemy")
            {
                Destroy(colliderHit.gameObject);
            }
        }
    }
}
