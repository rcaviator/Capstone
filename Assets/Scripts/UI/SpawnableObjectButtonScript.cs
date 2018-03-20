using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnableObjectButtonScript : MonoBehaviour
{
    GameObject storedGameObject;
    Text objectText;
    Image objectImage;

	// Use this for initialization
	void Awake ()
    {
        //set references
        objectText = transform.GetChild(0).GetComponent<Text>();
        objectImage = transform.GetChild(1).GetComponent<Image>();
	}
	

    public void SetGameObject(GameObject newGameObject)
    {
        storedGameObject = newGameObject;
        objectText.text = storedGameObject.name;
        if (storedGameObject.GetComponent<SpriteRenderer>())
        {
            objectImage.sprite = storedGameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            objectImage = null;
        }
    }


    public void OnSelectObjectClick()
    {
        //set selected game object and update the ui
        GameManager.Instance.EditorController.SelectedObject = storedGameObject;
        UIManager.Instance.SelectedObjectText.ChangeText(objectText.text);
        UIManager.Instance.SelectedObjectImage.ChangeImage(objectImage.sprite);
    }
}
