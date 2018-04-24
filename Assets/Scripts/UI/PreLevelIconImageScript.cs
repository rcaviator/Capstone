using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelIconImageScript : MonoBehaviour
{
    //image reference
    Image icon;

	// Use this for initialization
	void Awake()
    {
        UIManager.Instance.PreLevelIconImage = this;
        icon = GetComponent<Image>();
        icon.sprite = Resources.Load<Sprite>("Graphics/UI/SteampunkGUI/png/buttons/Yes (3)");
    }
	
	
    public void ChangeTitleIconImage(Sprite image)
    {
        icon.sprite = image;
    }
}
