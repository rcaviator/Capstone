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

        //draw grid
        grid = GetComponent<CustomGrid>();
        grid.Initialize();
        
        //set other references

    }

    /// <summary>
    /// Reset the game to not be in editor scene
    /// </summary>
    private void OnDestroy()
    {
        GameManager.Instance.IsLevelEditor = false;
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

    public void SetModuleLevelAndNumberOnCreate(int level, int number)
    {
        moduleLevel = level;
        moduleNumber = number;
        setupMenu.SetActive(false);
        grid.ClearGrid();

        //set module text reference
        UIManager.Instance.SelectedModuleText.ChangeText("Level: " + moduleLevel.ToString() + " Module: " + moduleNumber.ToString());
    }

    public void SetModuleLevelAndNumberOnLoadFile(int level, int number)
    {
        moduleLevel = level;
        moduleNumber = number;
    }


    public void ClearGrid()
    {
        grid.ClearGrid();
    }

    public void FillDirt()
    {
        grid.FillDirt();
    }

    public void FlipObject()
    {
        IsFlipped = !IsFlipped;
        UIManager.Instance.SelectedObjectImage.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void SaveModule()
    {
        grid.SaveModule(moduleLevel, moduleNumber);

        //hack to save begining or ending modules. comment out line above
        //begining and end modules are lvl 0
        //grid.SaveModule(0, 0);
        //grid.SaveModule(0, 1);

        //show save notification
        UIManager.Instance.SaveNotification.ShowNotification();
    }


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


    public void EditorToSetupMenuOnClick()
    {
        setupMenu.SetActive(true);
    }

    public void CloseSetupMenuOnClick()
    {
        setupMenu.SetActive(false);
    }

    private void PlaceSelectedObjectAt(Vector3 clickPoint)
    {
        clickPoint.z = 0f;
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);
        if (targetCell != null)
        {
            grid.SetGameObjectInGrid(targetCell, SelectedObject, IsFlipped);
        }
    }

    private void RemoveObjectAt(Vector3 clickPoint)
    {
        clickPoint.z = 0f;
        CustomGridCell targetCell = grid.GetGridCellInGrid(clickPoint);
        if (targetCell != null)
        {
            grid.RemoveGameObjectInGrid(targetCell);
        }
    }
}
