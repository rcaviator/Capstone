using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// enum for switching over the main menu menus
/// </summary>
public enum MainMenus
{
    None,

    Main, Settings, Credits,
}

public class MainMenuControllerScript : MonoBehaviour
{
    //dictionaries for getting and referencing the menu prefabs and gameobjects
    Dictionary<MainMenus, GameObject> uiDictPrefabs;
    Dictionary<MainMenus, GameObject> uiDict;


	// Use this for initialization
	void Awake ()
    {
        //set reference in UI manager
        UIManager.Instance.MainMenuControl = this;

        //load the prefabs into the prefab dictionary uiDictPrefabs
        uiDictPrefabs = new Dictionary<MainMenus, GameObject>()
        {
            { MainMenus.Main, Resources.Load<GameObject>("Prefabs/UI/Main menu/MainMenuCanvas") },
            { MainMenus.Settings, Resources.Load<GameObject>("Prefabs/UI/Main menu/SettingsCanvas") },
            { MainMenus.Credits, Resources.Load<GameObject>("Prefabs/UI/Main menu/CreditsCanvas") },
        };

        //create the uiDict
        uiDict = new Dictionary<MainMenus, GameObject>();

        //loop through the prefab dict and instantiate the prefabs into gameobjects
        //set gameobject clones into the uiDict, set their world cameras, and disable them all
        foreach (KeyValuePair<MainMenus, GameObject> entry in uiDictPrefabs)
        {
            GameObject u = Instantiate(entry.Value, Vector3.zero, Quaternion.identity);
            u.GetComponent<Canvas>().worldCamera = Camera.main;
            uiDict.Add(entry.Key, u);
            u.SetActive(false);
        }

        //enable the main menu
        if (uiDict.ContainsKey(MainMenus.Main))
        {
            uiDict[MainMenus.Main].SetActive(true);
        }

        //show error message if applicable
        if (GameManager.Instance.ErrorMessage != null)
        {
            GameObject error = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Main menu/ErrorMsgCanvas"), Vector3.zero, Quaternion.identity);
            error.GetComponent<Canvas>().worldCamera = Camera.main;
        }
	}
	
    /// <summary>
    /// Changes the menu to a new menu
    /// </summary>
    /// <param name="newMenu">the menu to change to</param>
	public void ChangeMenu(MainMenus newMenu)
    {
        //check if menu exists in dictionary
        if (!uiDict.ContainsKey(newMenu))
        {
            Debug.Log(newMenu + " is not in the uiDict dictionary!");
            return;
        }

        //loop through the dictionary and disable all the menus
        foreach (KeyValuePair<MainMenus, GameObject> entry in uiDict)
        {
            entry.Value.SetActive(false);
        }

        //enable the menu to change to
        uiDict[newMenu].SetActive(true);
    }
}
