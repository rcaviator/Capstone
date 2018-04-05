using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// enum  of editor setup menus
/// </summary>
public enum EditorSetupMenu
{
    None, Main, Load, New,
}

public class LevelEditorSetupScript : MonoBehaviour
{
    //menu references
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject loadMenu;
    [SerializeField]
    GameObject newMenu;

    //the dropdown fields
    [SerializeField]
    Dropdown moduleLevel;
    [SerializeField]
    Dropdown moduleNumber;

    //menu dictionary
    Dictionary<EditorSetupMenu, GameObject> setupMenu;

	// Use this for initialization
	void Awake ()
    {
        //initialize the menu dictionary
        setupMenu = new Dictionary<EditorSetupMenu, GameObject>()
        {
            { EditorSetupMenu.Main, mainMenu },
            { EditorSetupMenu.Load, loadMenu },
            { EditorSetupMenu.New, newMenu },
        };

        //disable all menus
        foreach (KeyValuePair<EditorSetupMenu, GameObject> menu in setupMenu)
        {
            menu.Value.SetActive(false);
        }

        //enable main menu
        setupMenu[EditorSetupMenu.Main].SetActive(true);
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}


    public void OnMainMenu()
    {
        ChangeMenu(EditorSetupMenu.Main);
    }


    public void OnLoadMenu()
    {
        ChangeMenu(EditorSetupMenu.Load);
    }


    public void OnNewMenu()
    {
        ChangeMenu(EditorSetupMenu.New);
    }

    public void CreateNewModule()
    {
        GameManager.Instance.EditorController.SetModuleLevelAndNumberOnCreate(moduleLevel.value + 1, moduleNumber.value + 1);
    }

    private void ChangeMenu(EditorSetupMenu newMenu)
    {
        //disable all menus
        foreach (KeyValuePair<EditorSetupMenu, GameObject> menu in setupMenu)
        {
            menu.Value.SetActive(false);
        }

        //enable new menu
        setupMenu[newMenu].SetActive(true);
    }

    private void OnEnable()
    {
        ChangeMenu(EditorSetupMenu.Main);
    }
}
