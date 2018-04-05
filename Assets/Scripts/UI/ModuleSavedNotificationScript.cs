using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class ModuleSavedNotificationScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        //set reference
        UIManager.Instance.SaveNotification = this;

        //hide it
        gameObject.SetActive(false);
	}


    public void ShowNotification()
    {
        gameObject.SetActive(true);
    }


    public void DismissNotification()
    {
        gameObject.SetActive(false);
    }
}
