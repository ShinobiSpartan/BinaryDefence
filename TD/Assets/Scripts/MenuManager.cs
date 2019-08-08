﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            if(!pausePanel.activeInHierarchy)
            {
                PauseButton();
            }
            else
                UnpauseGame();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void PlayMap1()
    {
        //change "Main" to "Map1"
        SceneManager.LoadScene("Main");
    }
   // public void PlayMap2()
   // {
   //     SceneManager.LoadScene("Map2");
   // }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Debug.Log("PAUSED");
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Debug.Log("UNPAUSED");
    }
    
  //  bool pauseToggled()
  //  {
  //      if(Time.timeScale == 0f)
  //      {
  //          Time.timeScale = 1f;
  //          return (false);
  //      }
  //      else
  //      {
  //          Time.timeScale = 0f;
  //          return (true);
  //      }
  //  }

}
