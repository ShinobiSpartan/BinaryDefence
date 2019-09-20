using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButton;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
               
        for (int i = 0; i < levelButton.Length; i++)
        {
            if(i + 1 < levelReached)
            {
                levelButton[i].interactable = false;
            }
        }   
    }

    /// <summary>
    /// Level selector
    /// loads the scene that gets labeled inside the inspector
    /// </summary>
    /// <param name="level"></param>
    public void Select (string level)
    {
        SceneManager.LoadScene(level);    
    }
}
