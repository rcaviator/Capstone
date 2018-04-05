using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorInstructionsButton : MonoBehaviour
{
    //chevron image reference
    Image chevron;

    //boolean to switch chevron
    bool chevronPointingUp = true;

	// Use this for initialization
	void Awake ()
    {
        //get the image reference
        chevron = transform.GetChild(0).GetComponent<Image>();
	}
	
    /// <summary>
    /// Rotates the image ui element after flipping a boolean
    /// </summary>
	public void OnShowHideClick()
    {
        chevronPointingUp = !chevronPointingUp;

        if (chevronPointingUp)
        {
            chevron.transform.Rotate(new Vector3(0, 0, 180));
        }
        else
        {
            chevron.transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}
