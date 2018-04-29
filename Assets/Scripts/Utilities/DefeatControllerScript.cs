using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatControllerScript : MonoBehaviour
{
    //defeat text
    [SerializeField]
    Text defeatText;

    //defeat icon
    [SerializeField]
    Image defeatImage;

    // Use this for initialization
    void Awake()
    {
        //player lost, set the score and inventory to before starting level
        GameManager.Instance.Score = GameManager.Instance.PreScore;
        GameManager.Instance.PlayerInventory.UseFallBackInventory();
	}


    private void Start()
    {
        //set the defeat text and image
        defeatText.text = GameManager.Instance.DeathObjectName;
        defeatImage.sprite = GameManager.Instance.DeathObjectSprite;

        //set the scale of the image
        float sizeX = defeatImage.sprite.bounds.size.x;
        float sizeY = defeatImage.sprite.bounds.size.y;
        if (sizeX > 5f || sizeY > 5f)
        {
            sizeX /= 2f;
            sizeY /= 2f;
        }
        defeatImage.transform.localScale = new Vector3(sizeX, sizeY, 1);
    }

    public void MainMenuButton()
    {
        //GameManager.Instance.PreScore = GameManager.Instance.Score;
        GameManager.Instance.PlayerInventory.UseFallBackInventory();
        GameManager.Instance.PlayerInventory.SaveInventory();
        GameManager.Instance.SaveGameData();
    }
}
