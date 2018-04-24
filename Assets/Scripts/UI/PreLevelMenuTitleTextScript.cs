using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelMenuTitleTextScript : MonoBehaviour
{
    //text reference
    Text menuText;

    // Use this for initialization
    private void Awake()
    {
        UIManager.Instance.PreLevelMenuTitleText = this;
        menuText = GetComponent<Text>();
        menuText.text = GameManager.Instance.Airports[GameManager.Instance.Level].AirportName;
    }

    public void ChangeTitleText(string msg)
    {
        menuText.text = msg;
    }
}
