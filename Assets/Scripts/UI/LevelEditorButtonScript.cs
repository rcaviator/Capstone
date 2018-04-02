using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorButtonScript : ButtonScript
{
    [SerializeField]
    bool firstTime;

    Button button;

	// Use this for initialization
	void Awake ()
    {
        //set reference
        button = GetComponent<Button>();
        if (firstTime)
        {
            button.interactable = false;
            firstTime = false;
        }
	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update();


	}

    public void MakeInteractable()
    {
        button.interactable = true;
    }

    //private void OnEnable()
    //{
    //    if (!firstTime)
    //    {
    //        button.interactable = true;
    //    }
    //}
}
