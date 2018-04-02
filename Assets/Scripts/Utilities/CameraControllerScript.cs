using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : PauseableObject
{
    //camera game speed variable
    float horizontalSpeed = 0f;

    //level editor movement controls
    Vector3 oldPos;
    Vector3 panOrigin;
    bool uiBlocker = false;
    //level editor clamping controls
    float leftBounds;
    float rightBounds;
    float topBounds;
    float bottomBounds;

    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        //set reference in game manager
        GameManager.Instance.PlayerCamera = this;

        leftBounds = 0f;
        bottomBounds = 0f;
        rightBounds = Constants.LEVEL_EDITOR_GRID_SIZE_X - 1;
        topBounds = Constants.LEVEL_EDITOR_GRID_SIZE_Y - 1;
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
            //enable pan and zoom if not over UI elements
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 50f))
            {
                //pan camera controls
                if (Input.GetMouseButtonDown(2) && !uiBlocker)
                {
                    //Get the ScreenVector the mouse clicked
                    oldPos = new Vector3(transform.position.x, transform.position.y, 0);//transform.position;
                    panOrigin = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
                    panOrigin.z = 0f;
                    uiBlocker = true;
                }
                if (Input.GetMouseButton(2) && uiBlocker)
                {
                    //Get the difference between where the mouse clicked and where it moved
                    Vector3 pos = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition) - panOrigin;
                    pos.z = 0f;
                    //Move the position of the camera to simulate a drag, speed * 3.5f for x and 2f for y for screen to worldspace conversion
                    transform.position = new Vector3(oldPos.x - pos.x * GetComponent<Camera>().orthographicSize * 3.5f, oldPos.y - pos.y * GetComponent<Camera>().orthographicSize * 2f, transform.position.z);//oldPos - pos;// * panSpeed;
                }
                if (Input.GetMouseButtonUp(2))
                {
                    uiBlocker = false;
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

            //camera clamping control
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBounds, rightBounds), Mathf.Clamp(transform.position.y, bottomBounds, topBounds), transform.position.z);
        }
	}

    /// <summary>
    /// Toggles the camera to slow to a halt
    /// </summary>
    public bool Landed
    { get; set; }
}
