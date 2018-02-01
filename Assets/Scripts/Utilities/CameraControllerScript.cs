using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : PauseableObject
{

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();

        //set reference in game manager
        GameManager.Instance.PlayerCamera = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //movement test
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rBody.velocity = new Vector2(rBody.velocity.x + 10 * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rBody.velocity = new Vector2(rBody.velocity.x - 10 * Time.deltaTime, 0f);
        }
	}
}
