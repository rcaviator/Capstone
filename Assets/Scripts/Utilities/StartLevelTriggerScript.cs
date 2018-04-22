using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTriggerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        //set reference
        GameManager.Instance.StartTrigger = this;
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change player mode and set reticle
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            GameManager.Instance.Player.State = PlayerScript.PlayerState.Manual;
            Instantiate(Resources.Load<GameObject>("Prefabs/Player/TargetReticle"), Vector3.zero, Quaternion.identity);
            UIManager.Instance.StatusPanel.EnableAndSetStatus(Constants.STATUS_GO_MESSAGE);
            UIManager.Instance.StatusPanel.StartCountdown();
        }
    }
}
