using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : PauseableObject
{
    float horizontalSpeed = 0f;

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
        if (!Landed)
        {
            horizontalSpeed = Mathf.Clamp(horizontalSpeed + Constants.CAMERA_SPEED * Time.deltaTime, 0f, Constants.CAMERA_SPEED);
            rBody.velocity = new Vector2(horizontalSpeed, 0f);
        }
        else
        {
            horizontalSpeed = Mathf.Clamp(horizontalSpeed - Constants.CAMERA_SPEED * 0.25f * Time.deltaTime, 0f, Constants.CAMERA_SPEED);
            rBody.velocity = new Vector2(horizontalSpeed, 0f);

            if (rBody.velocity.x <= 0f)
            {
                MySceneManager.Instance.ChangeScene(Scenes.LevelComplete);
            }
        }
	}

    /// <summary>
    /// Toggles the camera to slow to a halt
    /// </summary>
    public bool Landed
    { get; set; }
}
