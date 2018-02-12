using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //pause menu control
        if (InputManager.Instance.GetButtonDown(PlayerAction.PauseGame) && !UIManager.Instance.PauseMenu)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/Pause menu/PauseMenuCanvas"), Vector3.zero, Quaternion.identity);
        }
        //else if (UIManager.Instance.PauseMenu && InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        //{

        //}
    }
}
