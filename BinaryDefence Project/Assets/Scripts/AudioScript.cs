using UnityEngine;

public class AudioScript : MonoBehaviour
{
    //audio source for the menu Audio
    AudioSource menuAudio;
    
    bool playMusic;
    bool toggleChange;

    // Start is called before the first frame update
    void Start()
    {
        //fetching the audio from the gameobject
        menuAudio = GetComponent<AudioSource>();
        //checking to see if the toggle is set to true for the music to play at start-up
        playMusic = true;
    }

    // Update is called once per frame
    void Update()
    {
        //checking to see if the toggle is positive
        if (playMusic == true && toggleChange == true)
        {
            //play the attatched audio sauce
            menuAudio.Play();
            //making sure that the audio isn't played twice
            toggleChange = false;
        }
        //checking to see if toggle is set to false
        if(playMusic == false && toggleChange == false)
        {
            //stopping the audio
            menuAudio.Stop();
            //making sure that the audio doesn't play anymore
            toggleChange = false;
        }
    }

    private void OnGUI()
    {
        //switching the toggle to activate and deactivate the parent gameobject
        playMusic = GUI.Toggle(new Rect(10, 10, 100, 30), playMusic, "Play Music");
        //decting if there is a chance with the toggle
        if(GUI.changed)
        {
            //changing to true to show that there was a change in the toggle state
            toggleChange = true;
        }
    }
}
