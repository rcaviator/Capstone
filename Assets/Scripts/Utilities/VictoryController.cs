using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    ////victory notification text
    //[SerializeField]
    //Text victoryNotificationText;

	// Use this for initialization
	void Awake()
    {
        GameManager.Instance.FinishedGame = true;
	}
}
