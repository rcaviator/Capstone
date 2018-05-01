using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelSliderIconScript : MonoBehaviour
{
    //image reference
    Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    // Use this for initialization
    void Start ()
    {
        //change for boss level
        if (GameManager.Instance.Level < 4)
        {
            icon.sprite = Resources.Load<Sprite>("Graphics/Environment/Tower");
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>("Graphics/UI/Other/Skull And Crossbones");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
