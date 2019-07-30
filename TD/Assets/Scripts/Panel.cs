﻿using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    #region Variables
    public GameObject panelHideShow;
    int counter;
    #endregion
    /// <showHideComments>
    /// checking to see if the player has hidden the panel ingame or not
    /// and setting to active or not (SetActive(false/true)) when the player
    /// pushes the button
    /// </showHide>
    public void showHide()
    {
        counter++;
        if (counter % 2 == 1)
        {
            panelHideShow.gameObject.SetActive(false);
        }
        else
        {
            panelHideShow.gameObject.SetActive(true);
        }
    }

}