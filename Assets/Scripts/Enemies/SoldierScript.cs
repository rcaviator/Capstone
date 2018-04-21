using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_SOLDIER_HEALTH;

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //death from 0 health
            if (health <= 0f)
            {
                //Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
                GameManager.Instance.Score += Constants.ENEMY_SOLDIER_SCORE;
                Destroy(gameObject);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill self and damage player if player crashed into soldier
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //else take damage from player bullet
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerBullet]))
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
        }
    }
}
