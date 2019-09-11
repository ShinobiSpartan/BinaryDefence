using UnityEngine.SceneManagement;
using UnityEngine;
public class MenuManager : MonoBehaviour
{

    public GameObject pausePanel;

    /// <summary>
    /// disabling pause panel
    /// </summary>
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
    /// <summary>
    /// Quiting the current game that is being plaed
    /// </summary>
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// fully exiting the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// restarting the game
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
    /// goes back to the main menu
    /// </summary>
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// taking the player to the Lobby screen
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    /// <summary>
    /// Plays the first map
    /// </summary>
    public void PlayMap1()
    {
        //change "Main" to "Map1"
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// plays the second map
    /// </summary>
    public void PlayMap2()
    {
        SceneManager.LoadScene("Map2");
    }

    /// <summary>
    /// plays the third map
    /// </summary>
    public void PlayMap3()
    {
        SceneManager.LoadScene("map3");
    }

    /// <summary>
    ///plays the fourth map 
    /// </summary>
    public void PlayMap4()
    {
        SceneManager.LoadScene("map4");
    }

    public void PlayMap5()
    {
        SceneManager.LoadScene("map5");
    }


    /// <summary>
    /// pauses the game
    /// </summary>
    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Debug.Log("PAUSED");
    }

    /// <summary>
    /// unpauses the game
    /// </summary>
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Debug.Log("UNPAUSED");
    }
    
    /// <summary>
    /// if the game is paused it will time will stop completely
    /// till game is unpasued and set back to normal
    /// </summary>
    /// <returns></returns>
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
