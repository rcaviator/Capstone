using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteControllerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake()
    {
        //player won level, update prescore
        GameManager.Instance.PreScore = GameManager.Instance.Score;
	}

    public void MainMenuButton()
    {
        //GameManager.Instance.PreScore = GameManager.Instance.Score;
        GameManager.Instance.PlayerInventory.SaveInventory();
        GameManager.Instance.SaveGameData();
    }
}
