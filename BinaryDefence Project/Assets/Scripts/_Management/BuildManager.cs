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

    public NodeUI nodeUI;

    [Header("Turret Prefabs")]
    [Tooltip("Add the turret prefabs inside here! (BasicTurret goes here)")]
    public GameObject standardTurretPrefab;
    [Tooltip("Add the turret prefabs inside here! (AATurret goes here)")]
    public GameObject aaTurretPrefab;
    [Tooltip("Add the turret prefabs inside here! (RailTurret goes here)")]
    public GameObject railTurretPrefab;
    [Tooltip("Add the Refinery prefab here!")]
    public GameObject refineryPlacement;

    private TurretBluePrint turretToBuild;
    private Node nodeSelected;

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

    public bool HasMoney
    {
        get { return PlayerStats.money >= turretToBuild.costingValue; }
    }

    /// <summary>
    /// Selecting a Node to build on and checking if the selected node is the node
    /// </summary>
    /// <param name="node">The selected Node</param>
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

    /// <summary>
    /// Selecting the turret to build
    /// Making turretToBuild set to turret and then desleceting the current node
    /// </summary>
    /// <param name="turret"></param>
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    /// <summary>
    /// Deselecting current node and then hiding the UI for the turret
    /// </summary>
    public void DeselectNode()
    {
        nodeSelected = null;
        nodeUI.HideUI();
    }
    /// <summary>
    /// Getting the Turret to build and returning "turretToBuild"
    /// </summary>
    /// <returns></returns>
    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
