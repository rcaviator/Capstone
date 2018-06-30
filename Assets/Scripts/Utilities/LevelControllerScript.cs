using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        //initialize level generation
        //get level info
        int currentLevel = GameManager.Instance.Level;

        //game object to hold module objects and positioning
        GameObject moduleParent;

        //load first module, starting airport
        moduleParent = LoadModule(0, 0);
        if (!moduleParent)
        {
            Debug.Log("First module error, returning to main menu.");
            GameManager.Instance.ErrorMessage = "Error in loading first module. Reinstall the game or contact the developer to fix this.";
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
            return;
        }
        moduleParent.transform.position = Vector3.zero;

        //load level modules
        int modulePlacement = Constants.MODULE_LENGTH;
        for (int i = 1; i < 9; i++)//19 -> 9
        {
            moduleParent = LoadModule(currentLevel, i);
            if (!moduleParent)
            {
                Debug.Log("Module " + i + " error, returning to main menu.");
                GameManager.Instance.ErrorMessage = "Error in loading module " + i + ". Reinstall the game or contact the developer to fix this.";
                MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
                return;
            }
            moduleParent.transform.position = new Vector3(modulePlacement, 0, 0);
            modulePlacement += Constants.MODULE_LENGTH;
        }

        //load last module, ending airport
        moduleParent = LoadModule(0, 1);
        if (!moduleParent)
        {
            Debug.Log("Last module error, returning to main menu.");
            GameManager.Instance.ErrorMessage = "Error in loading last module. Reinstall the game or contact the developer to fix this.";
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
            return;
        }
        moduleParent.transform.position = new Vector3(modulePlacement, 0, 0);

        //create a ground layer below the level for custom module voids
        for (int i = -Constants.MODULE_LENGTH; i < Constants.MODULE_LENGTH * 11; i++)
        {
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlock), new Vector3(i, -5, 0), Quaternion.identity);
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlock), new Vector3(i, -6, 0), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //pause menu control
        if (InputManager.Instance.GetButtonDown(PlayerAction.PauseGame) && !UIManager.Instance.PauseMenu)
        {
            Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.PauseMenuCanvas), Vector3.zero, Quaternion.identity);
        }
        //else if (UIManager.Instance.PauseMenu && InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        //{

        //}
    }


    public GameObject LoadModule(int level, int number)
    {
        //create the parent module object for all loaded game objects to parent from.
        //parent is returned for easy transform positioning
        GameObject moduleParent = new GameObject("Module Parent. Level: " + level.ToString() + " Number: " + number.ToString());
        moduleParent.transform.position = Vector3.zero;

        //create file string
        string file;

        //set application path
        if (Directory.Exists(Application.dataPath + "/Modules/"))
        {
            //set file path
            file = Application.dataPath + "/Modules/" + "Level_" + level.ToString("D2") + "_Module_" + number.ToString("D2") + ".mod";

            //check if file exists
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
            //create reader
            using (BinaryReader r = new BinaryReader(s))
            {
                //verify file is of correct format
                string head = new string(r.ReadChars(4));
                if (!head.Equals(Constants.MODULE_FILE_HEADER))
                {
                    Debug.Log("File not of correct format in reading module for level generation.");
                    return null;
                }

                //get number of game objects
                int numberOfGameObjects = r.ReadInt32();

                //loop through the file and create each game object, add to grid, and move it
                for (int i = 0; i < numberOfGameObjects; i++)
                {
                    //get type
                    int typeInt = r.ReadInt32();
                    Constants.ObjectIDs type = (Constants.ObjectIDs)typeInt;

                    //check if flipped
                    bool flipped = r.ReadBoolean();

                    //get position
                    int x = r.ReadInt32();
                    int y = r.ReadInt32();

                    //create spawn game object reference
                    GameObject spawnObject = null;// = new GameObject();

                    //create object
                    switch (type)
                    {
                        case Constants.ObjectIDs.None:
                            break;

                        //utilities
                        case Constants.ObjectIDs.LevelStartPoint:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.LevelStartPoint), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.LevelEndPoint:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.LevelEndPoint), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        //environment - blocks
                        case Constants.ObjectIDs.DirtBlock:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlock), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.DirtBlockGrass:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlockGrass), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.DirtBlockSloped:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlockSloped), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.DirtBlockSlopedGrass:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.DirtBlockSlopedGrass), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.StoneBlock:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.StoneBlock), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.StoneBlockSloped:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.StoneBlockSloped), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.StoneBlockConcreteTop:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.StoneBlockConcreteTop), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.StoneBlockSlopedConcreteTop:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.StoneBlockSlopedConcreteTop), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        //environment - buildings
                        case Constants.ObjectIDs.HangarClose:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.HangarClose), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.HangarMiddle:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.HangarMiddle), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.HangarFar:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.HangarFar), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Tower:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Tower), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        //environment - weather
                        case Constants.ObjectIDs.WeatherHazard1:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.WeatherHazard1), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.WeatherHazard2:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.WeatherHazard2), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.WeatherHazard3:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.WeatherHazard3), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        //environment - other
                        case Constants.ObjectIDs.Bird:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Bird), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        //allies
                            

                        //enemies
                        case Constants.ObjectIDs.MotherShip:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Mothership), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Zepplin:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Zepplin), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Tank:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Tank), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Soldier:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Soldier), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Jeep:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Jeep), new Vector3(x, y, 0f), Quaternion.identity);
                            break;
                        case Constants.ObjectIDs.Bomber:
                            spawnObject = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.Bomber), new Vector3(x, y, 0f), Quaternion.identity);
                            break;

                        default:
                            Debug.Log("Invalid object being loaded");
                            spawnObject = new GameObject("Invalid object loaded");
                            break;
                    }

                    //set object properties
                    if (flipped)
                    {
                        spawnObject.transform.Rotate(new Vector3(0, 180, 0));
                    }

                    //set parent
                    spawnObject.transform.SetParent(null);
                    spawnObject.transform.SetParent(moduleParent.transform);
                }
            }
        }

        return moduleParent;
    }
}
