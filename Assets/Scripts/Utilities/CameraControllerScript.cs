using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : PauseableObject
{
    //camera game speed variable
    float horizontalSpeed = 0f;

    //level editor movement controls
    bool bDragging = true;
    Vector3 oldPos;
    Vector3 panOrigin;
    float panSpeed = 50f;

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
        //game code
        if (MySceneManager.Instance.CurrentScene == Scenes.GameLevel)
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
        //editor code
        else if (MySceneManager.Instance.CurrentScene == Scenes.LevelEditor)
        {
            
            //pan camera controls
            if (Input.GetMouseButtonDown(2))
            {
                //Get the ScreenVector the mouse clicked
                bDragging = true;
                oldPos = transform.position;
                panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(2))
            {
                //Get the difference between where the mouse clicked and where it moved
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
                //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
                transform.position = oldPos + -pos * panSpeed;
            }
            if (Input.GetMouseButtonUp(2))
            {
                bDragging = false;
            }

            //scrolling controls
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                //zoom in
                if (GetComponent<Camera>().orthographicSize > 1f)
                {
                    GetComponent<Camera>().orthographicSize--;
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                //zoom out
                if (GetComponent<Camera>().orthographicSize < 30f)
                {
                    GetComponent<Camera>().orthographicSize++;
                }
            }
            
        }
	}

    /// <summary>
    /// Toggles the camera to slow to a halt
    /// </summary>
    public bool Landed
    { get; set; }
}
