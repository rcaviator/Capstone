using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameCanvasScript : MonoBehaviour
{
    /// <summary>
    /// Used on the Yes button
    /// </summary>
	public void ConfirmedNewGame()
    {
        GameManager.Instance.Level = Constants.GAME_DEFAULT_LEVEL;
        GameManager.Instance.Score = Constants.GAME_DEFAULT_SCORE;
        GameManager.Instance.SaveGameData();
        GameManager.Instance.PlayerInventory.ResetInventory();
        GameManager.Instance.PlayerInventory.SaveInventory();
        MySceneManager.Instance.ChangeScene(Scenes.Tutorial);
    }

    /// <summary>
    /// Used on the No button
    /// </summary>
    public void CancelNewGame()
    {
        Destroy(gameObject);
    }
}
