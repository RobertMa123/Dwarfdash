using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level_Loader : MonoBehaviour
{
    public CharacterController controller;
    public Animator transition;
    
    // Update is called once per frame
    void Update()
    {
        if (controller.dead)
        {
            StartCoroutine(LoadLevel(0));
        
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(.5F);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title Screen");
    }
}
