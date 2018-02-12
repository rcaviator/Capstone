using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelButtonScript : ButtonScript
{
    //prelevel menu to change to if applicable. leave on None if n/a
    [SerializeField]
    PreLevelMenus preLevelMenuToGoTo;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();


	}


    public void OnPreLevelMenuChange()
    {
        UIManager.Instance.PreLevelMenuControl.ChangeMenu(preLevelMenuToGoTo);
    }
}
