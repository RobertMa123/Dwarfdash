using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class TitleScreen : MonoBehaviour
{
    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

    public void EasyMode()
    {
        SceneManager.LoadScene(1);
    }

    public void HardMode()
    {
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
