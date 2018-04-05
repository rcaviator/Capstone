using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectTextScript : MonoBehaviour
{
    //the text reference to display
    Text objectText;

	// Use this for initialization
	void Awake ()
    {
        //set references
        objectText = GetComponent<Text>();
        UIManager.Instance.SelectedObjectText = this;
	}

    /// <summary>
    /// Changes the UI text
    /// </summary>
    /// <param name="newText">the text to change to</param>
    public void ChangeText(string newText)
    {
        objectText.text = newText;
    }
}
