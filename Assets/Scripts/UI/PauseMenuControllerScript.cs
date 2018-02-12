using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum for switching in the pause menu menus
/// </summary>
public enum PauseMenuPanel
{
    None, Main, Settings,
}

public class PauseMenuControllerScript : MonoBehaviour
{
    //dictionary of the menus
    Dictionary<PauseMenuPanel, GameObject> uiDict;

	// Use this for initialization
	void Awake ()
    {
        //set references
        UIManager.Instance.PauseMenu = this;
        GameManager.Instance.Paused = true;

        //set the world camera
        GetComponent<Canvas>().worldCamera = Camera.main;

        //setup the pause menu dictionary
        uiDict = new Dictionary<PauseMenuPanel, GameObject>()
        {
            { PauseMenuPanel.Main, transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject },
            { PauseMenuPanel.Settings, transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject },
        };

        //disable them all
        foreach (KeyValuePair<PauseMenuPanel, GameObject> panels in uiDict)
        {
            panels.Value.SetActive(false);
        }

        //enable the main pause menu
        uiDict[PauseMenuPanel.Main].SetActive(true);

        //play sound
        //AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GamePaused);
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}


    private void OnDestroy()
    {
        GameManager.Instance.Paused = false;
    }

    /// <summary>
    /// Closes this menu
    /// </summary>
    public void ClosePauseMenu()
    {
        //AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameStart);
        Destroy(gameObject);
    }

    /// <summary>
    /// bypass for enums not showing in editor
    /// </summary>
    public void GoToMainPauseMenu()
    {
        //AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuForward);
        OnPanelChange(PauseMenuPanel.Main);
    }

    /// <summary>
    /// bypass for enums not showing in editor
    /// </summary>
    public void GoToSettingsPauseMenu()
    {
        //AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuForward);
        OnPanelChange(PauseMenuPanel.Settings);
    }

    /// <summary>
    /// Changes the pause menu panel
    /// </summary>
    /// <param name="panel">the panel to change to</param>
    public void OnPanelChange(PauseMenuPanel panel)
    {
        //close all panels and enable the passed panel
        foreach (KeyValuePair<PauseMenuPanel, GameObject> panels in uiDict)
        {
            panels.Value.SetActive(false);
        }

        //enable the one we want
        uiDict[panel].SetActive(true);
    }
}
