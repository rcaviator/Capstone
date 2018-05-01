using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialMenu
{
    None, Lore, Tutorial,
}

public class TutorialCanvasScript : MonoBehaviour
{
    //menu title text
    [SerializeField]
    Text menuTitleText;

    //lore panel
    [SerializeField]
    GameObject lorePanel;

    //tutorial panel
    [SerializeField]
    GameObject tutorialPanel;

    Dictionary<TutorialMenu, GameObject> tutorialMenuDict;

	// Use this for initialization
	void Awake()
    {
        //set reference
        UIManager.Instance.Tutorial = this;

        //initialize the dictionary
        tutorialMenuDict = new Dictionary<TutorialMenu, GameObject>()
        {
            { TutorialMenu.Lore, lorePanel },
            { TutorialMenu.Tutorial, tutorialPanel },
        };

        //disable them all
        foreach (KeyValuePair<TutorialMenu, GameObject> item in tutorialMenuDict)
        {
            tutorialMenuDict[item.Key].SetActive(false);
        }

        //enable the lore
        tutorialMenuDict[TutorialMenu.Lore].SetActive(true);
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}


    public void ChangeTutorialMenu(TutorialMenu newMenu)
    {
        //disable them all
        foreach (KeyValuePair<TutorialMenu, GameObject> item in tutorialMenuDict)
        {
            tutorialMenuDict[item.Key].SetActive(false);
        }

        //enable the new menu
        tutorialMenuDict[newMenu].SetActive(true);

        if (newMenu == TutorialMenu.Lore)
        {
            menuTitleText.text = "Lore";
        }
        else if (newMenu == TutorialMenu.Tutorial)
        {
            menuTitleText.text = "Tutorial";
        }
    }
}
