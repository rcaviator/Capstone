using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        //initialize managers
        GameManager.Instance.ToString();
        AudioManager.Instance.ToString();
        MySceneManager.Instance.ToString();
        InputManager.Instance.ToString();
	}
}
