using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorNotificationScript : MonoBehaviour
{
    //text reference
    Text errorText;

	// Use this for initialization
	void Awake()
    {
        if (GameManager.Instance.ErrorMessage != null)
        {
            errorText = transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
            errorText.text = GameManager.Instance.ErrorMessage;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
	
	public void CloseDialog()
    {
        GameManager.Instance.ErrorMessage = null;
        gameObject.SetActive(false);
    }
}
