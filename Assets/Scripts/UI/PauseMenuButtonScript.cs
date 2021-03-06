﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonScript : ButtonScript
{
    //pause menu to change to if applicable. leave on None if n/a
    [SerializeField]
    PauseMenuPanel pauseMenuToGoTo;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


    }

    public void OnPauseMenuChange()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        UIManager.Instance.PauseMenu.OnPanelChange(pauseMenuToGoTo);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        GameManager.Instance.Score = GameManager.Instance.PreScore;
        GameManager.Instance.PlayerInventory.UseFallBackInventory();
    }
}
