using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTriggerScript : MonoBehaviour
{
    bool firstPlayerEnter = true;

	// Use this for initialization
	void Awake ()
    {
        GameManager.Instance.EndTrigger = this;
	}
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set player mode and remove applicable objects
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            if (firstPlayerEnter)
            {
                firstPlayerEnter = false;
                GameManager.Instance.Player.PrepareForLanding();
                UIManager.Instance.StatusPanel.EnableAndSetStatus(Constants.STATUS_LEVEL_FINISHED_MESSAGE);
            }
        }
    }
}
