using UnityEngine;
using UnityEngine.EventSystems;

//This class is inside the 'Node' Prefab
public class Node : MonoBehaviour
{
    #region Variables
    [Header("Color")]
    //hover color of the item
    public Color hoverColor;
    //offset of position for turrets
    public Vector3 positionOffest;
    //color for not enough money
    public Color notEnoughMoney;

    [Header("Optional")] 
    //game object for the turret
    public GameObject turret;

    [HideInInspector]
    //blueprint for the turret
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    //is it upgradeable
    public bool isUpgraded = false;
    //renderer
    private Renderer rend;
    //singleton buildmanager
    BuildManager buildManager;
    #endregion

    private void Start()
    {

        rend = GetComponent<Renderer>();
        //setting buildManger to the instance of BuildManager
        buildManager = BuildManager.instance;
        //if buildManager is null
        if (buildManager == null)
            Debug.Log("Start");
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
        //taking money away from the costing value of the blueprint
        PlayerStats.money -= bluePrint.costingValue;
        //instantiating the blueprint prefab and allowing the "GetBuildPos()" to have a offset
        //keeping the identity
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPos(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        Vibration.Vibrate(50);

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

        if (buildManager == null)
            Debug.Log("OnMouseEnter");

        if (!buildManager.CanBuild)
            return;

        //checking if the player has the money to do that action
        //if the player does have enough money;
        //
        //set the render material color to the hoverColor
        //          OR
        //set the render material color to notEnoughMoney
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
