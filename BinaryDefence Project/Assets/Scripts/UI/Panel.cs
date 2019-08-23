﻿using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    #region Variables
    public GameObject panelHideShow;
    int counter;

    bool hideButton;
    #endregion
    /// <showHideComments>
    /// checking to see if the player has hidden the panel ingame or not
    /// and setting to active or not (SetActive(false/true)) when the player
    /// pushes the button
    /// </showHide>
    public void showHide()
    {

        hideButton = !hideButton;
        if (hideButton == true)
        {
            panelHideShow.gameObject.SetActive(false);
        }
        else
        {
            panelHideShow.gameObject.SetActive(true);
        }
    }

}
