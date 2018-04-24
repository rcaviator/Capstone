using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherBriefingTextScript : MonoBehaviour
{
    //get text reference
    Text weatherText;

    // Use this for initialization
    void Start()
    {
        weatherText = GetComponent<Text>();
        weatherText.text = GameManager.Instance.Airports[GameManager.Instance.Level].WeatherBriefing;
    }
}
