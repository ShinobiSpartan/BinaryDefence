using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int costingValue;

     public GameObject upgradedPrefab;
     public int upgradeCost;

    public int sellCost ()
    {
        return costingValue / 2;
    }
}
