using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectImageScript : MonoBehaviour
{
    //the object image to display
    Image objectImage;

	// Use this for initialization
	void Awake ()
    {
        //set references
        objectImage = GetComponent<Image>();
        UIManager.Instance.SelectedObjectImage = this;
	}
	
    public void ChangeImage(Sprite newImage)
    {
        objectImage.sprite = newImage;
    }
}
