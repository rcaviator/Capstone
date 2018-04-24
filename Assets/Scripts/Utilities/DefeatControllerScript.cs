using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatControllerScript : MonoBehaviour
{
    ////defeat text
    //Text defeatText;

    ////defeat icon
    //Image defeatImage;

	// Use this for initialization
	void Awake()
    {
        //player lost, set the score to before starting level
        GameManager.Instance.Score = GameManager.Instance.PreScore;
	}

    public void MainMenuButton()
    {
        //GameManager.Instance.PreScore = GameManager.Instance.Score;
        GameManager.Instance.PlayerInventory.SaveInventory();
        GameManager.Instance.SaveGameData();
    }
}
