using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombScript : ProjectilesScript
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
	}

    /// <summary>
    /// Initializes the player basic attack projectile
    /// </summary>
    /// <param name="bulletVelocity">the velocity vector</param>
    public void InitializeProjectile(Vector2 bulletVelocity)
    {
        //prep vector
        bulletVelocity.Normalize();
        bulletVelocity *= Constants.CLUSTER_BOMB_SPEED;

        //initialize the bullet info for the parent
        Initialize(bulletVelocity, Constants.CLUSTER_BOMB_LIFETIME);

        //set sprite rotation
        float angle = Mathf.Atan2(bulletVelocity.y, bulletVelocity.x) * Mathf.Rad2Deg + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

        //set relative velocity to player
        rBody.velocity = new Vector2(rBody.velocity.x + GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity.x, rBody.velocity.y + GameManager.Instance.Player.GetComponent<Rigidbody2D>().velocity.y);
    }


    private void OnDestroy()
    {
        Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
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
