using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    #region Variables
    [Tooltip("Set a panel here to hide")]
    public GameObject panelHideShow;
    [Tooltip("Set a button here to hide")]
    public GameObject buttonHideShow;

    bool hideButton;
    #endregion
    /// <showHideComments>
    /// checking to see if the player has hidden the panel ingame or not
    /// and setting to active or not (SetActive(false/true)) when the player
    /// pushes the button
    /// </showHide>
    public void showHide()
    {
        //setting hideButton to not hideButton
        hideButton = !hideButton;
        //if the hideButton is true, then set both panel & button to false
        //else set them to true
        if (hideButton == true)
        {
            //panel
            panelHideShow.gameObject.SetActive(false);
            //button
            buttonHideShow.gameObject.SetActive(false);
        }
        else
        {
            //panel
            panelHideShow.gameObject.SetActive(true);
            //button
            buttonHideShow.gameObject.SetActive(true);
        }
    }

}
