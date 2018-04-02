using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        ////temp ground generation code
        ////first airport
        //for (int i = 0; i < 40; i++)
        //{
        //    Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        //}
        ////main ground
        //for (int i = 40; i < 280; i++)
        //{
        //    Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Dirt_Block"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        //}
        ////second airport
        //for (int i = 280; i < 320; i++)
        //{
        //    Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Stone_Block_Concrete_Top"), new Vector3(i - 10, -5, 0), Quaternion.identity);
        //}

        //initialize level generation
        //get level info
        int currentLevel = GameManager.Instance.Level;

        GameObject moduleParent;

        //load first module, starting airport
        moduleParent = LoadModule(0, 0);
        if (!moduleParent)
        {
            Debug.Log("First module error, returning to main menu.");
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
        }
        moduleParent.transform.position = Vector3.zero;

        //load level modules
        int modulePlacement = Constants.MODULE_LENGTH;
        for (int i = 1; i < 19; i++)
        {
            moduleParent = LoadModule(currentLevel, i);
            if (!moduleParent)
            {
                Debug.Log("Module " + i + " error, returning to main menu.");
                MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
            }
            moduleParent.transform.position = new Vector3(modulePlacement, 0, 0);
            modulePlacement += Constants.MODULE_LENGTH;
        }

        //load last module, ending airport
        moduleParent = LoadModule(0, 1);
        if (!moduleParent)
        {
            Debug.Log("Last module error, returning to main menu.");
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
        }
        moduleParent.transform.position = new Vector3(modulePlacement, 0, 0);

        //level loaded, enable player
        GameManager.Instance.Player.gameObject.SetActive(true);
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


    public GameObject LoadModule(int level, int number)
    {
        //create the parent module object for all loaded game objects to parent from.
        //parent is returned for easy transform positioning
        GameObject moduleParent = new GameObject("Module Parent. Level: " + level + " Number: " + number);
        moduleParent.transform.position = Vector3.zero;

        //create file string
        string file;

        //set application path
        if (!Directory.Exists(Application.dataPath + "/Modules"))
        {
            //set file path
            file = Application.dataPath + "/Modules" + "/Level_" + level.ToString() + "_Module_" + number.ToString() + ".mod";

            //check if it exists
            if (!File.Exists(file))
            {
                Debug.Log("Module file does not exist in directory: " + level + " " + number);
                return null;
            }
            
        }
        else
        {
            Debug.Log("Module directory does not exist!");
            return null;
        }

        //procede if module exists
        using (Stream s = File.OpenRead(file))
        {

        }

        return moduleParent;
    }
}
