using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    #region Variables
    [Header("UI Element")]
    //game object for the ui
    public GameObject ui;
    //node for the target
    private Node target;
    [Header("Upgrading/Selling")]
    //text for the upgrade cost
    public Text upgradeCost;
    //text for the sell cost
    public Text sellCost;
    //button for the upgrade button
    public Button upgradeButton;
    #endregion

    public void SetTarget(Node _target)
    {
        //setting target to _target
        target = _target;
        //setting the position for the targets build position
        transform.position = target.GetBuildPos() + new Vector3(-1.34f, 1.49f, -0.06f);

        if(!target.isUpgraded)
        {
            //setting the upgrade cost of the structure that is being upgraded 
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            //making the Upgrade button not interactable
            upgradeCost.text = "N/A";
            upgradeButton.interactable = false;

        }

        //setting the sell cost of the structure that is being sold
        //then dividing the costing value of the turret by 2
        sellCost.text = "$" + target.turretBluePrint.costingValue / 2;
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
       // beenUpgraded = true;
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
