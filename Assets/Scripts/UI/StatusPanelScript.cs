using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanelScript : MonoBehaviour
{
    //text reference
    Text statusText;

    //timer
    float timer = 0f;
    bool countdown = false;

	// Use this for initialization
	void Awake()
    {
        UIManager.Instance.StatusPanel = this;
        statusText = transform.GetChild(0).GetComponent<Text>();
        EnableAndSetStatus(Constants.STATUS_TAKEOFF_MESSAGE);
	}

    private void Update()
    {
        if (!GameManager.Instance.Paused)
        {
            if (countdown)
            {
                if (timer <= Constants.STATUS_PANEL_TIMER)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    DisableStatus();
                }
            }
        }
    }


    public void DisableStatus()
    {
        gameObject.SetActive(false);
        countdown = false;
        timer = 0f;
    }


    public void EnableAndSetStatus(string msg)
    {
        gameObject.SetActive(true);
        statusText.text = msg;
    }


    public void StartCountdown()
    {
        countdown = true;
    }
}
