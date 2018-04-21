using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// enum for the menu buttons in the editor
/// </summary>
public enum LevelEditorMenus
{
    None,

    MainSetupMenu, LoadModule, NewModule,
}

public class LevelEditorControllerScript : MonoBehaviour
{
    //setup menu panel reference
    [SerializeField]
    GameObject setupMenu;
    
    //the main grid
    CustomGrid grid;

    //the module level apearance
    int moduleLevel;

    //the module order number
    int moduleNumber;

    //the object to be placed in the grid
    GameObject selectedObject;

	// Use this for initialization
	void Awake ()
    {
        //set game manager's editor level
        GameManager.Instance.EditorController = this;
        GameManager.Instance.IsLevelEditor = true;

        //create grid
        grid = GetComponent<CustomGrid>();
        grid.Initialize();
    }

    // Update is called once per frame
    void Update ()
    {
        //allow interaction after setup menu is disabled
        if (!setupMenu.activeInHierarchy)
        {
            //place object if not over ui
            if (Input.GetMouseButton(0))
            {
                Ray ray = GameManager.Instance.PlayerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit, 50f))
                {
                    PlaceSelectedObjectAt(GameManager.Instance.PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else if (Input.GetMouseButton(1))
            {
                RemoveObjectAt(GameManager.Instance.PlayerCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    /// <summary>
    /// Reset the game to not be in editor scene
    /// </summary>
    private void OnDestroy()
    {
        GameManager.Instance.IsLevelEditor = false;
    }

    /// <summary>
    /// Is the setup menu open
    /// </summary>
    public bool IsSetupMenuOpen
    { get; set; }

    /// <summary>
    /// The game object currently selected for placement
    /// </summary>
    public GameObject SelectedObject
    {
        get
        {
            return selectedObject;
        }
        set
        {
            selectedObject = value;

            //by default do not make the object flipped
            IsFlipped = false;
            UIManager.Instance.SelectedObjectImage.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Is the selected object rotated around the y axis?
    /// </summary>
    public bool IsFlipped
    { get; set; }

    /// <summary>
    /// Sets the level number and module number in the level editor
    /// for saving reference on new creation.
    /// </summary>
    /// <param name="level">The level of this module</param>
    /// <param name="number">The number of this module's order</param>
    public void SetModuleLevelAndNumberOnCreate(int level, int number)
    {
        //set level and module numbers
        moduleLevel = level;
        moduleNumber = number;

        //disable the menu
        setupMenu.SetActive(false);

        //prep the grid for new content
        grid.ClearGrid();

        //set module text reference
        UIManager.Instance.SelectedModuleText.ChangeText("Level: " + moduleLevel.ToString() + " Module: " + moduleNumber.ToString());
    }

    /// <summary>
    /// Sets the level number and module number in the level editor
    /// for saving reference on load file.
    /// </summary>
    /// <param name="level">The level of this module</param>
    /// <param name="number">The number of this module's order</param>
    public void SetModuleLevelAndNumberOnLoadFile(int level, int number)
    {
        moduleLevel = level;
        moduleNumber = number;
    }

    /// <summary>
    /// Flips the selected object for mirrored placement.
    /// </summary>
    public void FlipObject()
    {
        IsFlipped = !IsFlipped;
        UIManager.Instance.SelectedObjectImage.transform.Rotate(new Vector3(0, 180, 0));
    }

    /// <summary>
    /// Clears the grid in the custom grid object.
    /// </summary>
    public void ClearGrid()
    {
        grid.ClearGrid();
    }

    /// <summary>
    /// Fills dirt blocks in the custom grid object.
    /// </summary>
    public void FillDirt()
    {
        grid.FillDirt();
    }

    /// <summary>
    /// Saves the module in the custom grid object.
    /// </summary>
    public void SaveModule()
    {
        grid.SaveModule(moduleLevel, moduleNumber);

        //hack to save begining or ending modules. comment out line above
        //begining and end modules are lvl 0
        //grid.SaveModule(0, 0);
        //grid.SaveModule(0, 1);
    }

    /// <summary>
    /// Loads the module with the provided file string name to the custom grid object.
    /// Saves the level and module number in the level controller.
    /// </summary>
    /// <param name="fileName">The name of the file</param>
    public void LoadModule(string fileName)
    {
        //set level and number
        string numbers = new string(fileName.Where(Char.IsDigit).ToArray());
        moduleLevel = Int32.Parse(numbers[0].ToString() + numbers[1].ToString());
        moduleNumber = Int32.Parse(numbers[2].ToString() + numbers[3].ToString());

        //set module text reference
        UIManager.Instance.SelectedModuleText.ChangeText("Level: " + moduleLevel.ToString() + " Module: " + moduleNumber.ToString());

        grid.LoadModule(fileName);
        setupMenu.SetActive(false);
    }

    /// <summary>
    /// Attempts to place the selected object at the given vector3 click point on the grid.
    /// </summary>
    /// <param name="clickPoint">The point on the grid of the mouse click</param>
    private void PlaceSelectedObjectAt(Vector3 clickPoint)
    {
        //set the z to 0f
        clickPoint.z = 0f;

        //get the target cell from world point location
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);

        //if the target cell is inside the grid and it's empty, place object
        if (targetCell != null && !targetCell.IsOccupied)
        {
            grid.SetGameObjectInGrid(targetCell, SelectedObject, IsFlipped);
        }
    }

    /// <summary>
    /// Attempts to remove an object at the given vector3 click point on the grid.
    /// </summary>
    /// <param name="clickPoint">The point on the grid of the mouse click</param>
    private void RemoveObjectAt(Vector3 clickPoint)
    {
        //set the z to 0f
        clickPoint.z = 0f;

        //get the target cell from world point location
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);

        //if the target cell is inside the grid and it's occupied, remove object
        if (targetCell != null && targetCell.IsOccupied)
        {
            grid.RemoveGameObjectInGrid(targetCell);
        }
    }

    /// <summary>
    /// Used for the setup button to return to the setup menu.
    /// </summary>
    public void EditorToSetupMenuOnClick()
    {
        setupMenu.SetActive(true);
    }

    /// <summary>
    /// Used for the return to editor button in the setup menu.
    /// </summary>
    public void CloseSetupMenuOnClick()
    {
        setupMenu.SetActive(false);
    }
}
