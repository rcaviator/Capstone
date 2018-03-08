using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSaveButtonScript : MonoBehaviour
{
    Text moduleSaveText;

	// Use this for initialization
	void Awake ()
    {
        moduleSaveText = transform.GetChild(0).GetComponent<Text>();
	}


    public int ModuleLevel
    { get; set; }


    public int ModuleNumber
    { get; set; }

    public void ModuleFileText(string name)
    {
        moduleSaveText.text = name;
    }


    public void OnModuleClick()
    {
        GameManager.Instance.EditorController.LoadModule(moduleSaveText.text);
    }
}
