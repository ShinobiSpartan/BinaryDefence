using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
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
