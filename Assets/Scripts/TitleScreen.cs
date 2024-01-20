using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class TitleScreen : MonoBehaviour
{

    public Animator transition;

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

    public void EasyMode()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void HardMode()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }
}
