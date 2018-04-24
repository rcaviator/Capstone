using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelBriefingTextScript : MonoBehaviour
{
    //get text reference
    Text briefingText;

	// Use this for initialization
	void Start()
    {
        briefingText = GetComponent<Text>();
        briefingText.text = GameManager.Instance.Airports[GameManager.Instance.Level].MissionBriefing;
	}
}
