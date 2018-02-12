using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSliderScript : MonoBehaviour
{
    Slider distanceProgressSlider;
    float xdistance = 0f;

	// Use this for initialization
	void Start ()
    {
        //set reference in game manager
        GameManager.Instance.ProgressSlider = this;

        //get slider component
        distanceProgressSlider = GetComponent<Slider>();

        //set x distance value for difference in start and finish trigger locations
        xdistance = GameManager.Instance.EndTrigger.transform.position.x - GameManager.Instance.StartTrigger.transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //update the distance slider based on the player's position between the level endpoints
        distanceProgressSlider.value = (GameManager.Instance.Player.transform.position.x - GameManager.Instance.StartTrigger.transform.position.x) / xdistance;
	}
}
