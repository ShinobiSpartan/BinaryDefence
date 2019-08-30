using UnityEngine;
using UnityEngine.EventSystems;

//This class is inside the 'Node' Prefab
public class Node : MonoBehaviour
{
    #region Variables
    [Header("Color")]
    public Color hoverColor;

    public Vector3 positionOffest;
    public Color notEnoughMoney;

    [Header("Optional")]
    public GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;

    BuildManager buildManager;
    #endregion

    private void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.instance;
    }

    /// <summary>
    /// This will return the transforms position and add the positions off set.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBuildPos()
    {
        return transform.position + positionOffest;
    }

    void OnMouseDown()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //checking to see if the turret is null
        if(turret != null)
        {
            buildManager.selecetNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;
        //Lets build things here!
        BuildTurret(buildManager.GetTurretToBuild());
    }

   void BuildTurret(TurretBluePrint bluePrint)
   {
        //checking to see if the player has the correct amount of money 
        if (PlayerStats.money < bluePrint.costingValue)
        {
            return;
        }

        PlayerStats.money -= bluePrint.costingValue;

        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPos(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;
   }

    /// <summary>
    /// Upgrading the turret
    /// </summary>
    public void UpgradeTurret()
    {
        //checking to see if the player has the correct amount of money to upgrade
        if (PlayerStats.money < turretBluePrint.upgradeCost)
        {
            return;
        }

        PlayerStats.money -= turretBluePrint.upgradeCost;

        //get rid of old turret
        Destroy(turret);

        //building a new turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPos(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;
    }

    /// <summary>
    /// Selling turrets
    /// Communicates with the "PlayerStats" class
    /// Once the player sells ther turret, they will get the money back and it will destroy whatever the current turret is
    /// </summary>
    public void SellTurret()
    {
        PlayerStats.money += turretBluePrint.sellCost();

        //destroying whatever turret is placed on the node
        Destroy(turret);
        turretBluePrint = null;
    }



    void OnMouseEnter()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoney;
        }

        rend.material.color = hoverColor;
    }

}
