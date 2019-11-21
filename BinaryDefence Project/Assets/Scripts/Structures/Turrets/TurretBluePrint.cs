using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    #region Variables
    [Header("Base Turret")]
    //game object for prefab
    public GameObject prefab;
    //costing value for the turret
    public int costingValue;

    [Header("Upgraded Turret")]
    //game object for the upgraded prefab
    public GameObject upgradedPrefab;
    //upgrade costing value for the turret
    public int upgradeCost;
    #endregion

    public int sellCost ()
    {
        //getting the costing value of the structure / 2
        return costingValue / 2;
    }
}
