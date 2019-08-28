using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint aaTurret;
    public TurretBluePrint railgunTurret;
    public TurretBluePrint refinery;

    BuildManager buildManager;
    
    void Start()
    {
        buildManager = BuildManager.instance;
    }


    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret bought.");
        //setting the turret that is being built to the "Standard Turret" prefab
        buildManager.SelectTurretToBuild(standardTurret);
    }

    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void SelectAATurret()
    {
        Debug.Log("AA Turret bought.");
        buildManager.SelectTurretToBuild(aaTurret);
    }

    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void SelectRailTurret()
    {
        Debug.Log("Rail Turret bought.");
        buildManager.SelectTurretToBuild(railgunTurret);
    }

    public void SelectRefinery()
    {
        Debug.Log("Refinery has been bought.");
        buildManager.SelectTurretToBuild(refinery);
    }

}
