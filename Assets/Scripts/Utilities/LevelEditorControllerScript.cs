using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum LevelEditorMenus
{
    None,

    MainSetupMenu, LoadModule, NewModule,
}

public class LevelEditorControllerScript : MonoBehaviour
{
    ////dictionary menus
    Dictionary<LevelEditorMenus, GameObject> setupMenu;
    [SerializeField]
    GameObject setupMenuMain;
    [SerializeField]
    GameObject setupMenuLoad;
    [SerializeField]
    GameObject setupMenuNew;

    [SerializeField]
    GameObject dirt;

    Text titleText;

    CustomGrid grid;

	// Use this for initialization
	void Awake ()
    {
        //set game manager's editor level
        GameManager.Instance.IsLevelEditor = true;
        //Debug.Log(GameManager.Instance.IsLevelEditor);

        //set UImanager referenece
        UIManager.Instance.LevelEditorController = this;

        //load UI elements
        setupMenu = new Dictionary<LevelEditorMenus, GameObject>()
        {
            { LevelEditorMenus.MainSetupMenu, setupMenuMain },
            { LevelEditorMenus.LoadModule, setupMenuLoad },
            { LevelEditorMenus.NewModule, setupMenuNew },
        };

        foreach (KeyValuePair<LevelEditorMenus, GameObject> menu in setupMenu)
        {
            menu.Value.SetActive(false);
        }

        if (setupMenu.ContainsKey(LevelEditorMenus.MainSetupMenu))
        {
            setupMenu[LevelEditorMenus.MainSetupMenu].SetActive(true);
        }

        //draw grid
        grid = GetComponent<CustomGrid>();
        grid.Initialize();
        
        //set other references

    }


    private void OnDestroy()
    {
        GameManager.Instance.IsLevelEditor = false;
        //Debug.Log(GameManager.Instance.IsLevelEditor);
    }


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceCubeNear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RemoveCubeNear(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        {
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
        }
    }

    /// <summary>
    /// The game object currently selected for placement
    /// </summary>
    public GameObject SelectedObject
    { get; set; }

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
