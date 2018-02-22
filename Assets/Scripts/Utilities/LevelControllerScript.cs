using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        //temp ground generation code
        //first airport
        for (int i = 0; i < 40; i++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        }
        //main ground
        for (int i = 40; i < 280; i++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        }
        //second airport
        for (int i = 280; i < 320; i++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        }
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
