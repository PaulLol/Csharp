using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonsRestart : MonoBehaviour
{
    public void Retart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_scene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
