using UnityEngine;

//This class is inside the 'Node' Prefab
public class Node : MonoBehaviour
{
    #region Variables
    public Color hoverColor;
    public Vector3 positionOffest;

    private GameObject turret;

    private Renderer rend;
    private Color startingColor;
    #endregion
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("Cannot build here! --> Make UI element to display on screen.");
            return;
        }

        //Lets build things here!
        //This will go into the BuildManager script and call the 'getTurretToBuild' 
        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffest, transform.rotation);

    }

    void OnMouseEnter()
    {
       rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
    }



}
