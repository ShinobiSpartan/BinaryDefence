using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    #region Variables
    [Header("UI Element")]
    public GameObject ui;

    private Node target;
    [Header("Upgrading/Selling")]
    public Text upgradeCost;
    public Text sellCost;
    #endregion

    public void SetTarget(Node _target)
    {
        target = _target;
        //setting the position for the targets build position
        transform.position = target.GetBuildPos();
        //setting the upgrade cost of the structure that is being upgraded 
        upgradeCost.text = "$" + target.turretBluePrint.costingValue;
        //setting UI to true
        ui.SetActive(true);
    }
    /// <summary>
    /// hiding the UI and setting to false
    /// </summary>
    public void HideUI()
    {
        ui.SetActive(false);
    }

    /// <summary>
    /// upgrading the turret and then deselecting the menu
    /// </summary>
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    /// <summary>
    /// selling the turret and then deselecting the menu
    /// </summary>
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
