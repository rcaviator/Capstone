using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedModuleTextScript : MonoBehaviour
{
    //the text reference to display
    Text moduleText;

	// Use this for initialization
	void Awake ()
    {
        //set references
        moduleText = GetComponent<Text>();
        UIManager.Instance.SelectedModuleText = this;
	}
	
    /// <summary>
    /// Changes the UI text
    /// </summary>
    /// <param name="newText">the text to change to</param>
	public void ChangeText(string newText)
    {
        moduleText.text = newText;
    }
}
