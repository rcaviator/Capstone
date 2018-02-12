using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTriggerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        GameManager.Instance.EndTrigger = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set player mode and remove applicable objects

        if (collision.tag == "Player")
        {
            //Destroy(GameManager.Instance.Reticle.gameObject);
            MySceneManager.Instance.ChangeScene(Scenes.MainMenu);
        }
    }
}
