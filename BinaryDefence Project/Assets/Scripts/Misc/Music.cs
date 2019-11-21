using UnityEngine;

public class Music : MonoBehaviour
{
    //a static Music instance
    private static Music instance = null;
    //static Instance
    public static Music Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        //checking to see if the instance is not set to null and current instance
        if (instance != null && instance != this)
        {
            //destroying the gameobject
            Destroy(this.gameObject);
            return;
        }
        else
        {
            //setting the instance to the current instance
            instance = this;
        }

        //this will keep the audio playing until paused
        DontDestroyOnLoad(this.gameObject);
    }

}
