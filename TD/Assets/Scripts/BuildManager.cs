using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //Everytime the game starts there should be only one "BuildManager"
    //and it should call the "Awake" method and can be accessed from anyone/anything
    //ONLY HAVE 1 BUILD MANAGER ATTATCHED TO UNITY!!!!
    #region Singleton
    //Singleton Pattern
    //access it without the class making this the central part of the build manager by
    //storing a referance to itself.

    public static BuildManager instance;
    #endregion
    #region Variables
    [Header("Turret Prefabs")]
    [Tooltip("Add the turret prefabs inside here! (BasicTurret goes here)")]
    public GameObject standardTurretPrefab;
    [Tooltip("Add the turret prefabs inside here! (AATurret goes here)")]
    public GameObject aaTurretPrefab;
    [Tooltip("Add the turret prefabs inside here! (RailTurret goes here)")]
    public GameObject railTurretPrefab;

    private TurretBluePrint turretToBuild;
    private Node nodeSelected;

    public NodeUI nodeUI;

    #endregion

    private void Awake()
    {
        if(instance!=null)
        {
            Debug.Log("There is another 'BuildManager' script in scene!(Remove one)");
        }

        instance = this;
    }

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public void buildTurretOn(Node node)
    {
        if(PlayerStats.money < turretToBuild.costingValue)
        {
            Debug.Log("YOU'RE POOR AF!");
            return;
        }

        PlayerStats.money -= turretToBuild.costingValue;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPos(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret Built! Remaining money: " + PlayerStats.money);
    }

    public void selecetNode(Node node)
    {
        if(nodeSelected == node)
        {
            DeselectNode();
            return;
        }


        nodeSelected = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void DeselectNode()
    {
        nodeSelected = null;
        nodeUI.HideUI();
    }
}
