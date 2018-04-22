using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonScript : ButtonScript
{
    //main menu to change to if applicable. leave on None if n/a
    [SerializeField]
    MainMenus mainMenuToGoTo;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


    }


    public void OnMainMenuChange()
    {
        UIManager.Instance.MainMenuControl.ChangeMenu(mainMenuToGoTo);
    }

    /// <summary>
    /// Used on New Game button
    /// </summary>
    public void CheckNewGame()
    {
        if (GameManager.Instance.Level > Constants.GAME_DEFAULT_LEVEL)
        {
            GameObject prompt = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Main menu/NewGameCanvas"), Vector3.zero, Quaternion.identity);
            prompt.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        else
        {
            MySceneManager.Instance.ChangeScene(Scenes.Tutorial);
        }
    }
}
