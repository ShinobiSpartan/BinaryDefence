using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Variables
    //turret blue print for standardTurret
    public TurretBluePrint standardTurret;
    //turret blue print for aaTurret
    public TurretBluePrint aaTurret;
    //turret blue print for railgunTurret
    public TurretBluePrint railgunTurret;
    //turret blue print for refinery
    public TurretBluePrint refinery;

    BuildManager buildManager;
    #endregion
    void Start()
    {
        //setting buildManager to the instance of BuildManager
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
        //setting the turret that is being built to the "AA Turret" prefab
        buildManager.SelectTurretToBuild(aaTurret);
    }

    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void SelectRailTurret()
    {
        Debug.Log("Rail Turret bought.");
        //setting the turret that is being built to the "Rail Turret" prefab
        buildManager.SelectTurretToBuild(railgunTurret);
    }
    //getting called from UI element, communicating with BuildManager
    //and currency amount
    public void SelectRefinery()
    {
        Debug.Log("Refinery has been bought.");
        //setting the turret that is being built to the "Refinery" prefab
        buildManager.SelectTurretToBuild(refinery);
    }

}
