using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject cameraStop;
    public GameObject unitStop;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    cameraStop.SetActive(false);
        //    unitStop.SetActive(false);
        //    isPaused = true;
        //    pauseMenu.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(0);
            cameraStop.SetActive(true);
            unitStop.SetActive(true);
            isPaused = true;
        }


        if (Input.GetKeyDown(KeyCode.F3))
        {
            Application.Quit();
            cameraStop.SetActive(true);
            unitStop.SetActive(true);
            isPaused = true;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                
                cameraStop.SetActive(true);
                unitStop.SetActive(true);
                isPaused = false;
                pauseMenu.SetActive(false);

            }
            else
            {
                cameraStop.SetActive(false);
                unitStop.SetActive(false);
                isPaused = true;
                pauseMenu.SetActive(true);

            }
        }

    }

    //public void MainMenu()
    //{
    //    cameraStop.SetActive(false);
    //    unitStop.SetActive(false);
    //    SceneManager.LoadScene(0);
    //}

    //public void ResumeGame()
    //{
    //    cameraStop.SetActive(true);
    //    unitStop.SetActive(true);
    //    isPaused = false;
    //    pauseMenu.SetActive(false);
    //}

    //public void QuitGame()
    //{
    //    Application.Quit();
    //}

}
