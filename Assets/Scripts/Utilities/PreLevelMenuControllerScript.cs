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

        //create the navigation menu canvas
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/MenuNavigationCanvas"), Vector3.zero, Quaternion.identity);
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
    }
}
