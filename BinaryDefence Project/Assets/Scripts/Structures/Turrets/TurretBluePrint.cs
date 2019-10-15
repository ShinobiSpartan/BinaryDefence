using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    #region Variables
    [Header("First Turret")]
    public GameObject prefab;
    public int costingValue;

    [Header("Second(Upgraded) Turret")]
    public GameObject upgradedPrefab;
    public int upgradeCost;
    #endregion

    public int sellCost ()
    {
        //getting the costing value of the structure / 2
        return costingValue / 2;
    }
}
