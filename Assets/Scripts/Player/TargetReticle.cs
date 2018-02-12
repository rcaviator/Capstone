using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReticle : MonoBehaviour
{
    Vector3 thisPosition;

	// Use this for initialization
	void Awake ()
    {
        GameManager.Instance.Reticle = this;
        thisPosition = new Vector3();
	}
	
	// Update is called once per frame
	void Update ()
    {
        thisPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(thisPosition.x, thisPosition.y, 0f);
	}
}
