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
        //This will go into the BuildManager script and call the 'getTurretToBuild' 
        buildManager.buildTurretOn(this);
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
