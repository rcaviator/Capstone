using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : PauseableObject
{
    //trigger boolean
    bool isInRange = false;

    //timer
    float timer = 0f;

    //health
    float health = Constants.BIRD_HEALTH;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //range and flight check
        if (isInRange)
        {
            if (timer <= Constants.BIRD_LIFETIME)
            {
                if (transform.rotation.eulerAngles.y == 180)
                {
                    rBody.velocity = new Vector2(Constants.BIRD_SPEED, 0f);
                }
                else
                {
                    rBody.velocity = new Vector2(-Constants.BIRD_SPEED, 0f);
                }
                
                timer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //health check
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.Health -= Constants.BIRD_DAMAGE;
            //AudioManager sound
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
        }
    }
}
