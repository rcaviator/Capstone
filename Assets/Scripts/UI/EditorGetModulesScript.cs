using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine.UI;

public class EditorGetModulesScript : MonoBehaviour
{
    //list of all modules
    List<GameObject> files = new List<GameObject>();

    //button prefab reference
    GameObject moduleButton;

    //boolean to show hidden modules
    bool showHiddenModules = Constants.IS_DEVELOPER_BUILD;

    /// <summary>
    /// Refreshes the list after creating any new modules in the level editor at run time.
    /// </summary>
    private void OnEnable()
    {
        foreach (GameObject ob in files)
        {
            Destroy(ob);
        }

        Initialize();
    }

    /// <summary>
    /// Initialize gets all the .mod files and sets each one as a button for a scroll view content panel.
    /// </summary>
    private void Initialize()
    {
        //set prefab reference
        moduleButton = Resources.Load<GameObject>("Prefabs/UI/Level editor/ModuleFileButton");

        //locate and number how many module files there are
        string folder = Application.dataPath + "/Modules";
        DirectoryInfo dir = new DirectoryInfo(folder);
        FileInfo[] info = dir.GetFiles("*.mod");

        //create each save file button
        foreach (FileInfo f in info)
        {
            //check if module is a hidden module
            if (f.Name == "Level_00_Module_00.mod" || f.Name == "Level_00_Module_01.mod")
            {
                if (!showHiddenModules)
                {
                    Debug.Log("found hidden module");
                    continue;
                }
            }
            
            GameObject b = Instantiate(moduleButton);
            b.transform.SetParent(null);
            b.transform.SetParent(gameObject.transform);
            b.GetComponent<ModuleSaveButtonScript>().ModuleFileText(f.Name);
            //fix for stupid scaling issues
            b.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            files.Add(b);
        }
    }
}
