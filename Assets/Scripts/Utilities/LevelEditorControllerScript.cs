using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


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

    //test object
    [SerializeField]
    GameObject dirt;

    Text titleText;

    //the main grid
    CustomGrid grid;

    //the module level apearance
    int moduleLevel;

    //the module order number
    int moduleNumber;

    //the module file path and name
    string fileName;

	// Use this for initialization
	void Awake ()
    {
        //set game manager's editor level
        GameManager.Instance.EditorController = this;
        GameManager.Instance.IsLevelEditor = true;

        //set UImanager referenece
        UIManager.Instance.LevelEditorController = this;

        //draw grid
        grid = GetComponent<CustomGrid>();
        grid.Initialize();
        
        //set other references

    }


    private void OnDestroy()
    {
        GameManager.Instance.IsLevelEditor = false;
    }


    // Update is called once per frame
    void Update ()
    {
        if (!setupMenu.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceCubeNear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RemoveCubeNear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        

        //if (InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        //{
        //    setupMenu.SetActive(true);
        //}
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
    { get; set; }


    public void SetModuleLevelAndNumberOnCreate(int level, int number)
    {
        moduleLevel = level;
        moduleNumber = number;
        setupMenu.SetActive(false);
        grid.ClearGrid();
    }

    public void SetModuleLevelAndNumberOnLoadFile(int level, int number)
    {
        moduleLevel = level;
        moduleNumber = number;
    }


    public void SaveModule()
    {
        grid.SaveModule(moduleLevel, moduleNumber);
    }


    public void LoadModule(string fileName)
    {
        //set level and number
        string numbers = new string(fileName.Where(Char.IsDigit).ToArray());
        moduleLevel = Int32.Parse(numbers[0].ToString());
        moduleNumber = Int32.Parse(numbers[1].ToString());

        grid.LoadModule(fileName);
        setupMenu.SetActive(false);
    }


    public void EditorToSetupMenuOnClick()
    {
        setupMenu.SetActive(true);
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        clickPoint.z = 0f;
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);
        if (targetCell != null)
        {
            grid.SetGameObjectInGrid(targetCell, dirt);
        }
    }

    private void RemoveCubeNear(Vector3 clickPoint)
    {
        clickPoint.z = 0f;
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);
        if (targetCell != null)
        {
            grid.RemoveGameObjectInGrid(targetCell);
        }
    }
}
