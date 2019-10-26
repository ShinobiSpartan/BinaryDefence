using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject pausePanel;

    /// <summary>
    /// Disabling pause panel
    /// </summary>
    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        //checking if keybind "p" is pressed to pause game
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
    /// <summary>
    /// Quitting the current game that is being played
    /// </summary>
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Fully exiting the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarting the game
    /// </summary>
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
    }
  
    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Debug.Log("PAUSED");
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Debug.Log("UNPAUSED");
    }
    
    /// <summary>
    /// If the game is paused it will time will stop completely
    /// till the game is unpasued and set back to normal
    /// </summary>
    bool pauseToggled()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }

}
