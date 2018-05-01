using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtonScript : ButtonScript
{
    //menu associated with button
    [SerializeField]
    TutorialMenu tutorialMenu;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Changes the tutorial menu by calling the tutorial canvas script
    /// </summary>
    public void OnTutorialMenuChange()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        UIManager.Instance.Tutorial.ChangeTutorialMenu(tutorialMenu);
    }
}
