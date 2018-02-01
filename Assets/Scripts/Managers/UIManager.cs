//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using UnityEngine;

/// <summary>
/// enum for which set of menus to use
/// </summary>
public enum MenuSet
{
    //default
    None,

    //main menus
    MainMenus,

    //game menus
    GameMenus,
}

/// <summary>
/// enum for the menus
/// </summary>
public enum Menus
{
    //default
    None,

    //main menu elements


    //game elements

}

/// <summary>
/// UIManager handles all the UI components in the game
/// </summary>
class UIManager
{
    #region Fields

    //singleton instance of class
    static UIManager instance;

    //the enum for which set of menus the game is using
    MenuSet currentMenuSet;

    //dictionary to hold reference to which set of menus the game is using
    Dictionary<Menus, GameObject> currentMenus;

    //dictionary to hold all the main menus
    Dictionary<Menus, GameObject> mainMenus;

    //dictionary to hold all the game menus
    Dictionary<Menus, GameObject> gameMenus;

    //the active menu reference
    GameObject activeMenu;

    #endregion

    #region Constructor

    /// <summary>
    /// private constructor
    /// </summary>
    private UIManager()
    {
        //initialize the main menu dictionary
        mainMenus = new Dictionary<Menus, GameObject>()
        {

        };

        //initialize the game menu dictionary
        gameMenus = new Dictionary<Menus, GameObject>()
        {

        };

        //set current menu set
        currentMenuSet = MenuSet.MainMenus;

        //set current menus dictionary
        currentMenus = mainMenus;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns the instance
    /// </summary>
    public static UIManager Instance
    {
        get { return instance ?? (instance = new UIManager()); }
    }

    /// <summary>
    /// Used for button referencing
    /// </summary>
    public MainMenuControllerScript MainMenuControl
    { get; set; }

    /// <summary>
    /// Used for button referencing
    /// </summary>
    public PreLevelMenuControllerScript PreLevelMenuControl
    { get; set; }

    /// <summary>
    /// Gets the current menu set in use
    /// </summary>
    public MenuSet CurrentMenuSet
    { get { return currentMenuSet; } }

    #endregion

    #region Methods

    /// <summary>
    /// Update is called from GameManager
    /// </summary>
    public void Update()
    {
        
    }

    /// <summary>
    /// ChangeMenuSets is for changing menu sets between main menus and gameplay.
    /// </summary>
    /// <param name="newSet">the set to change to</param>
    public void ChangeMenuSets(MenuSet newSet)
    {
        //set menu set enum
        currentMenuSet = newSet;

        //change current menus dictionary
        switch (newSet)
        {
            case MenuSet.None:
                break;
            case MenuSet.MainMenus:
                currentMenus = mainMenus;
                break;
            case MenuSet.GameMenus:
                currentMenus = gameMenus;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// OpenMenus opens the menu from the passed menus enum if applicable
    /// </summary>
    /// <param name="menu">the menu to open</param>
    public void OpenMenu(Menus menu)
    {
        //check if menu is in the current menus dictionary
        if (currentMenus.ContainsKey(menu))
        {
            //set active menu reference and instantiate
            activeMenu = MonoBehaviour.Instantiate(currentMenus[menu], new Vector3(0, 0, 0), Quaternion.identity);
            
            //set camera

        }
        else
        {
            Debug.Log(menu.ToString() + " is not in the UIManager current menu dictionary!");
        }
    }

    /// <summary>
    /// Closes the ui
    /// </summary>
    void CloseUI()
    {
        if (activeMenu)
        {
            MonoBehaviour.Destroy(activeMenu);
        }
    }

    #endregion
}
