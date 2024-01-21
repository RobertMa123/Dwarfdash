using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class cinemachineControl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public float lockedTime;
    private bool islockedAtPos;
    public float speedOfFix;

    private float baseXPosition;

    private void Start()
    {

    }

    private void Update()
    {
        if (islockedAtPos != true)
        {
            Vector3 newFollowPos = new Vector3(0, player.transform.position.y, 0);
            Vector3 followLerped = Vector3.Lerp(gameObject.transform.position, newFollowPos, Time.deltaTime * speedOfFix);
            gameObject.transform.position = newFollowPos;
        }
    }

    public void lockAtPosition()
    {
        islockedAtPos = true;
        StopAllCoroutines();
        StartCoroutine(timer());
        
    }

    public IEnumerator timer()
    {
        float timer = 0;

        while (timer < lockedTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        islockedAtPos = false;
    }
}
