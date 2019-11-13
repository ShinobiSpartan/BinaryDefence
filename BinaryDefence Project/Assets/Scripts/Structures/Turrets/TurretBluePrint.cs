using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    #region Variables
    [Header("Base Turret")]
    public GameObject prefab;
    public int costingValue;

    [Header("Upgraded Turret")]
    public GameObject upgradedPrefab;
    public int upgradeCost;
    #endregion

    public int sellCost ()
    {
        //getting the costing value of the structure / 2
        return costingValue / 2;
    }
}
