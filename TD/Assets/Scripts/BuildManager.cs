using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //Everytime the game starts there should be only one "BuildManager"
    //and it should call the "Awake" method and can be accessed from anyone/anything
    //ONLY HAVE 1 BUILD MANAGER ATTATCHED TO UNITY!!!!
    #region Singleton
    //Singleton Pattern
    //access it without the class making this the central part of the build manager by
    //storing a referance to itself.

    public static BuildManager instance;
    #endregion

    private void Awake()
    {
        if(instance!=null)
        {
            Debug.Log("There is another 'BuildManager' script in scene!(Remove one)");
        }

        instance = this;
    }

    public GameObject standardTurretPrefab;

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject getTurretToBuild()
    {
        return turretToBuild;
    }



}
