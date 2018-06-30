using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : PauseableObject
{
    //main health
    float health = Constants.ENEMY_SOLDIER_HEALTH;

    //gravity toggle
    bool hitGround = false;

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
            //place the object on the ground in the game scene
            if (MySceneManager.Instance.CurrentScene == Scenes.GameLevel)
            {
                if (!hitGround)
                {
                    rBody.velocity = new Vector2(0, Physics2D.gravity.y);
                }
                else
                {
                    rBody.velocity = Vector2.zero;
                }
            }

            //death from 0 health
            if (health <= 0f)
            {
                //Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
                GameManager.Instance.Score += Constants.ENEMY_SOLDIER_SCORE;
                Destroy(gameObject);
            }
        }
	}


    public void ModifyHealth(float amount)
    {
        health -= amount;
        //flashHealthBar = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill self and damage player if player crashed into soldier
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            Destroy(gameObject);
        }
        //else take damage from player bullet
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerBullet]))
        {
            health -= Constants.PLAYER_BASIC_BULLET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.PlayerAdvancedBullet]))
        {
            health -= Constants.PLAYER_ADVANCED_BULLET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.ClusterBomb]))
        {
            health -= Constants.CLUSTER_BOMB_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyBeam]))
        {
            health -= Constants.ENERGY_BEAM_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyShield]))
        {
            GameManager.Instance.Score += Constants.ENEMY_SOLDIER_SCORE;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.LightningBolt]))
        {
            health -= Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE;
        }
        //setup on ground
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Ground]))
        {
            hitGround = true;
        }
    }
}
