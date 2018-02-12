using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTriggerScript : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        GameManager.Instance.StartTrigger = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change player mode and set reticle

        if (collision.tag == "Player")
        {
            GameManager.Instance.Player.State = PlayerScript.PlayerState.Manual;
            Instantiate(Resources.Load<GameObject>("Prefabs/Player/TargetReticle"), Vector3.zero, Quaternion.identity);
        }

        
    }
}
