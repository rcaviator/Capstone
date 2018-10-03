using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyProjectileShellScript : ProjectilesScript
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.GunFire2);

        //Instantiate(Resources.Load<GameObject>("Prefabs/Effects/ModerateExplosion"), transform.position, Quaternion.identity);
    }


    /// <summary>
    /// Initializes the player basic attack projectile
    /// </summary>
    /// <param name="bulletVelocity">the velocity vector</param>
    public void InitializeProjectile(Vector2 bulletVelocity)
    {
        //prep vector
        bulletVelocity.Normalize();
        bulletVelocity *= Constants.HEAVY_PROJECTILE_SHELL_SPEED;

        //initialize the bullet info for the parent
        Initialize(bulletVelocity, Constants.HEAVY_PROJECTILE_SHELL_LIFETIME);

        //set sprite rotation
        float angle = Mathf.Atan2(bulletVelocity.y, bulletVelocity.x) * Mathf.Rad2Deg - 180;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    private void OnDestroy()
    {
        Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            //Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.ModerateExplosion), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.ClusterBomb]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyBeam]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnergyShield]))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.SeekerMissile]))
        {
            Destroy(gameObject);
        }
    }
}
