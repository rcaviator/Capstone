using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    Text scoreText;

	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
    }


    private void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
    }
}
