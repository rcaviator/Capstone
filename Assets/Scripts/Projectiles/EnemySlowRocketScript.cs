using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowRocketScript : ProjectilesScript
{
    float health;

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

        health = Constants.ENEMY_SLOW_ROCKET_HEALTH;
        AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.RocketFire1);
        Initialize(Vector2.zero, Constants.ENEMY_SLOW_ROCKET_LIFETIME);
	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

        if (!GameManager.Instance.Paused)
        {
            //look-at rotation logic
            Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //movement logic
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Player.transform.position, Constants.ENEMY_SLOW_ROCKET_SPEED * Time.deltaTime);

            //health
            if (health <= 0)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
                GameManager.Instance.Score += Constants.ENEMY_SLOW_ROCKET_SCORE;
                Destroy(gameObject);
            }
        }
	}

    protected override void Initialize(Vector2 velocity, float bulletTimer)
    {
        base.Initialize(velocity, bulletTimer);
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
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
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            health -= Constants.SEEKER_MISSILES_DAMAGE;
        }
    }
}
