using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSavedNotificationScript : MonoBehaviour
{
    //text reference
    [SerializeField]
    Text notificationText;

	// Use this for initialization
	void Awake ()
    {
        //set reference
        UIManager.Instance.SaveNotification = this;

        //hide it
        gameObject.SetActive(false);
	}

    /// <summary>
    /// Shows a successful save notification to the player
    /// </summary>
    public void ShowNotification()
    {
        gameObject.SetActive(true);
        notificationText.text = "Module Saved!";
        GameManager.Instance.Paused = true;
    }

    /// <summary>
    /// Closes the save notification
    /// </summary>
    public void DismissNotification()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Paused = false;
    }

    /// <summary>
    /// Shows an error in saving notification to the player
    /// </summary>
    public void ShowErrorNotification()
    {
        gameObject.SetActive(true);
        notificationText.text = "Error: No objects in the grid to save!\nPlace Objects in the grid before saving.";
        GameManager.Instance.Paused = true;
    }
}
