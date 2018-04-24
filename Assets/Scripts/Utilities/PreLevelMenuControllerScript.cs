using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// enum for switching pre level menus
/// </summary>
public enum PreLevelMenus
{
    None,

    Overview, Shop, WeatherAndMap,
}

public class PreLevelMenuControllerScript : MonoBehaviour
{
    //dictionaries for getting and referencing the menu prefabs and gameobjects
    Dictionary<PreLevelMenus, GameObject> uiDictPrefabs;
    Dictionary<PreLevelMenus, GameObject> uiDict;

    // Use this for initialization
    void Awake ()
    {
        //set reference in UI manager
        UIManager.Instance.PreLevelMenuControl = this;

        //instantiate the title canvas
        GameObject titleCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/PreLevelMenuTitleCanvas"), Vector3.zero, Quaternion.identity);
        titleCanvas.GetComponent<Canvas>().worldCamera = Camera.main;

        //load the prefabs into the prefab dictionary uiDictPrefabs
        uiDictPrefabs = new Dictionary<PreLevelMenus, GameObject>()
        {
            { PreLevelMenus.Overview, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/OverviewCanvas") },
            { PreLevelMenus.Shop, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/ShopCanvas") },
            { PreLevelMenus.WeatherAndMap, Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/WeatherAndMapCanvas") },
        };

        //create the uiDict
        uiDict = new Dictionary<PreLevelMenus, GameObject>();

        //loop through the prefab dict and instantiate the prefabs into gameobjects
        //set gameobject clones into the uiDict, set their world cameras, and disable them all
        foreach (KeyValuePair<PreLevelMenus, GameObject> entry in uiDictPrefabs)
        {
            GameObject u = Instantiate(entry.Value, Vector3.zero, Quaternion.identity);
            u.GetComponent<Canvas>().worldCamera = Camera.main;
            uiDict.Add(entry.Key, u);
            u.SetActive(false);
        }

        //enable the main menu
        if (uiDict.ContainsKey(PreLevelMenus.Overview))
        {
            uiDict[PreLevelMenus.Overview].SetActive(true);
        }

        //create the navigation menu canvas and set camera
        GameObject nav = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/MenuNavigationCanvas"), Vector3.zero, Quaternion.identity);
        nav.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    /// <summary>
    /// Changes the menu to a new menu
    /// </summary>
    /// <param name="newMenu">the menu to change to</param>
    public void ChangeMenu(PreLevelMenus newMenu)
    {
        //check if menu exists in dictionary
        if (!uiDict.ContainsKey(newMenu))
        {
            Debug.Log(newMenu + " is not in the uiDict dictionary!");
            return;
        }

        //loop through the dictionary and disable all the menus
        foreach (KeyValuePair<PreLevelMenus, GameObject> entry in uiDict)
        {
            entry.Value.SetActive(false);
        }

        //enable the menu to change to
        uiDict[newMenu].SetActive(true);

        //update the title text and image
        switch (newMenu)
        {
            case PreLevelMenus.None:
                break;
            case PreLevelMenus.Overview:
                UIManager.Instance.PreLevelMenuTitleText.ChangeTitleText(GameManager.Instance.Airports[GameManager.Instance.Level].AirportName);
                UIManager.Instance.PreLevelIconImage.ChangeTitleIconImage(Resources.Load<Sprite>("Graphics/UI/SteampunkGUI/png/buttons/Yes (3)"));
                break;
            case PreLevelMenus.Shop:
                UIManager.Instance.PreLevelMenuTitleText.ChangeTitleText("Airport Shop");
                UIManager.Instance.PreLevelIconImage.ChangeTitleIconImage(Resources.Load<Sprite>("Graphics/UI/SteampunkGUI/png/buttons/Shop (3)"));
                break;
            case PreLevelMenus.WeatherAndMap:
                UIManager.Instance.PreLevelMenuTitleText.ChangeTitleText("Next Stop:\n" + GameManager.Instance.Airports[GameManager.Instance.Level].NextAirportName);
                UIManager.Instance.PreLevelIconImage.ChangeTitleIconImage(Resources.Load<Sprite>("Graphics/UI/SteampunkGUI/png/custom/Lightning"));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Saves the score to prescore and saves the inventory in GM.
    /// Prevents score farming
    /// </summary>
    public void TakeoffFinalizer()
    {
        GameManager.Instance.PreScore = GameManager.Instance.Score;
    }

    /// <summary>
    /// Saves the players progress if returning to main menu
    /// </summary>
    public void MainMenuButton()
    {
        GameManager.Instance.PreScore = GameManager.Instance.Score;
        GameManager.Instance.PlayerInventory.SaveInventory();
        GameManager.Instance.SaveGameData();
    }
}
