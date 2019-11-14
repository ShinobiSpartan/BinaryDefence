using UnityEngine;

public class PauseMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Pausing the music
        Music.Instance.gameObject.GetComponent<AudioSource>().Pause();                
    }
}
