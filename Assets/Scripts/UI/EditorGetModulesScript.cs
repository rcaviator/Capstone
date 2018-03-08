using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditorGetModulesScript : MonoBehaviour
{
    List<GameObject> files = new List<GameObject>();

    GameObject moduleButton;

	// Use this for initialization
	void Start()
    {
        
	}

    private void OnEnable()
    {
        foreach (GameObject ob in files)
        {
            Destroy(ob);
        }

        Initialize();
    }

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
            //Debug.Log(f.FullName);
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
