using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    
    void Start()
    {
        buildManager = BuildManager.instance;
    }


    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void BuyStandardTurret()
    {
        Debug.Log("Standard Turret bought.");
        //setting the turret that is being built to the "Standard Turret" prefab
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void BuyAATurret()
    {
        Debug.Log("AA Turret bought.");
        buildManager.SetTurretToBuild(buildManager.aaTurretPrefab);
    }

    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void BuyRailTurret()
    {
        Debug.Log("Rail Turret bought.");
        buildManager.SetTurretToBuild(buildManager.railTurretPrefab);
    }
}
