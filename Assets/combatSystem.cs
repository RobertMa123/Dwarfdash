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
    }

    private void attack()
    {
        //When attacking, check the overlap circle and then
        Collider2D[] colliderHit = Physics2D.OverlapCircleAll(transform.position, attackArea);

        foreach (Collider2D collider in colliderHit)
        {
            if (collider != null)
            {
                if (collider.gameObject.tag == "enemy")
                {
                    Debug.Log("hit");
                    Destroy(collider.gameObject);
                    character.boostOnKill();
                    camControl.lockAtPosition();
                }
            }
        }
    }
}
