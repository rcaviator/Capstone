using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMissileScript : ProjectilesScript
{
    //enemy target
    GameObject enemyTarget;

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

	}
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //look-at rotation logic
            if (enemyTarget != null)
            {
                Vector3 dir = enemyTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //movement logic
                transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, Constants.SEEKER_MISSILES_SPEED * Time.deltaTime);
            }
            else
            {
                Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
	}

    public void Initialize(GameObject target, Vector2 vel, float missileLife)
    {
        base.Initialize(vel, missileLife);

        //set target
        enemyTarget = target;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Jeep]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.MotherShip]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Soldier]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Tank]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Zepplin]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyFastRocket]))
        {
            
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemySlowRocket]))
        {
            
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bird]))
        {

        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.HeavyProjectileShell]))
        {
            
        }
    }
}
