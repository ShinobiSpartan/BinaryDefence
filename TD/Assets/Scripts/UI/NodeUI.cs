using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;

    public Text upgradeCost;
    public Text sellCost;


    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPos();

        upgradeCost.text = "$" + target.turretBluePrint.costingValue;
        //sellCost.text = "$" + target.turretBluePrint.sellCost;

        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    /// <summary>
    /// upgrading the turret and then deslecting the menu
    /// </summary>
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
