using UnityEngine;
using UnityEngine.EventSystems;

//This class is inside the 'Node' Prefab
public class Node : MonoBehaviour
{
    #region Variables
    public Color hoverColor;
    public Vector3 positionOffest;

    private GameObject turret;

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

    void OnMouseDown()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.getTurretToBuild() == null)
            return;
        
        if(turret != null)
        {
            Debug.Log("Cannot build here! --> Make UI element to display on screen.");
            return;
        }

        //Lets build things here!
        //This will go into the BuildManager script and call the 'getTurretToBuild' 
        GameObject turretToBuild = buildManager.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffest, transform.rotation);

    }

    void OnMouseEnter()
    {
        //checking if the player is hovering over a UI element which is in the way
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.getTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
    }



}
