using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : PauseableObject
{
    //range trigger boolean
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
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //health check
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
	}


    private void FixedUpdate()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
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

                    timer += Time.fixedDeltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
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
        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            Destroy(gameObject);
        }
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
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]))
        {
            health -= Constants.ENEMY_BOMBER_COLLISION_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.HeavyProjectileShell]))
        {
            health -= Constants.HEAVY_PROJECTILE_SHELL_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyFastRocket]))
        {
            health -= Constants.ENEMY_FAST_ROCKET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemySlowRocket]))
        {
            health -= Constants.ENEMY_SLOW_ROCKET_DAMAGE;
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.LightningBolt]))
        {
            health -= Constants.WEATHER_HAZARD_2_LIGHTING_DAMAGE;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player is in range, start timer and advance
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            isInRange = true;
        }
    }
}
