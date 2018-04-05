using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorInstructionsScript : MonoBehaviour
{
    //the starting position
    Vector3 startPosition;

    //boolean for switching up or down
    bool isShown = true;

	// Use this for initialization
	void Awake ()
    {
        //set starting position reference
        startPosition = GetComponent<RectTransform>().anchoredPosition3D;
	}
	
    /// <summary>
    /// Shows or hides the instructions ui after flipping the show boolean
    /// </summary>
	public void OnShowHideClick()
    {
        isShown = !isShown;

        if (isShown)
        {
            GetComponent<RectTransform>().anchoredPosition3D = startPosition;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition3D = new Vector3(GetComponent<RectTransform>().anchoredPosition3D.x, GetComponent<RectTransform>().anchoredPosition3D.y - (GetComponent<RectTransform>().rect.y * 2), GetComponent<RectTransform>().anchoredPosition3D.z);
        }
    }
}
