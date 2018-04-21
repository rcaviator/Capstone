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
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set player mode and remove applicable objects
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            if (firstPlayerEnter)
            {
                firstPlayerEnter = false;
                //Destroy(GameManager.Instance.Reticle.gameObject);
                //MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
                //GameManager.Instance.Player.State = PlayerScript.PlayerState.AutoPilotLanding;
                GameManager.Instance.Player.PrepareForLanding();
            }
        }
    }
}
