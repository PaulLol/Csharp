using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMain : MonoBehaviour
{
    public MoveSnake mainSnake;
    public void Resume()
    {
        mainSnake = GameObject.FindGameObjectWithTag("SnakeMain").GetComponent<MoveSnake>();
        Time.timeScale = 1;
        mainSnake.canvas.SetActive(false);
    }
    public void ToMM()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
