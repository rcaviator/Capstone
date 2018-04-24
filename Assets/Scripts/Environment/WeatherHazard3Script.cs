using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHazard3Script : PauseableObject
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

    }
	
	// Update is called once per frame
	void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {

        }
	}
}
