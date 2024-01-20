using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Options")]
    public float speed;
    public float multiplier;

<<<<<<< Updated upstream
=======
    public float baseDelayMultiplier;

>>>>>>> Stashed changes
    public Transform player;


    void Start()
    {
    }
    void Update()
    {
        float delayMultiplier = baseDelayMultiplier * Vector2.Distance(player.transform.position, Input.mousePosition);
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.position = Vector2.Lerp(player.position, mousePos, Time.deltaTime * speed * multiplier);
    }
}
