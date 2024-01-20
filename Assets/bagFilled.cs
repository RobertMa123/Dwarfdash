using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bagFilled : MonoBehaviour
{
    public float fillPercentage;
    public CharacterController player;
    private Animator thisAnimator;

    public Image uiImage;

    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        fillPercentage = 1 - player.currentSpeedPos;
        uiImage.fillAmount = fillPercentage;

        if (fillPercentage < 0.45)
        {
            thisAnimator.SetBool("isStressed", true);
        }
        else
        {
            thisAnimator.SetBool("isStressed", false);
        }
    }
}
