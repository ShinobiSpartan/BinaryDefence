using UnityEngine;
using UnityEngine.EventSystems;

//This class is inside the 'Node' Prefab
public class Node : MonoBehaviour
{
    #region Variables
    public Color hoverColor;
    public Vector3 positionOffest;

    [Header("Optional")]
    public GameObject turret;

    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startingColor;

    BuildManager buildManager;
    #endregion

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPos()
    {
        return transform.position + positionOffest;
    }

    void OnMouseDown()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        
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
        if (PlayerStats.money < bluePrint.costingValue)
        {
            Debug.Log("YOU'RE POOR AF!");
            return;
        }

        PlayerStats.money -= bluePrint.costingValue;

        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPos(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        Debug.Log("Turret Built! Remaining money: " + PlayerStats.money);
   }

    /// <summary>
    /// Upgrading the turret
    /// </summary>
    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBluePrint.upgradeCost)
        {
            Debug.Log("No money to upgrade!");
            return;
        }

        PlayerStats.money -= turretBluePrint.upgradeCost;

        //get rid of old turret
        Destroy(turret);

        //building a new turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPos(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;

        Debug.Log("Turret upgraded! Remaining money: " + PlayerStats.money);
    }

    public void SellTurret()
    {
        //PlayerStats.money += turretBluePrint.cost * 0.5f;
    }



    void OnMouseEnter()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
    }



}
