using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBulletScript : ProjectilesScript
{
    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.GunFire1);
    }

    /// <summary>
    /// Initializes the player basic attack projectile
    /// </summary>
    /// <param name="bulletVelocity">the velocity vector</param>
    public void InitializePlayerBasicProjectile(Vector2 bulletVelocity)
    {
        //prep vector
        bulletVelocity.Normalize();
        bulletVelocity *= Constants.PLAYER_BASIC_BULLET_SPEED;

        //initialize the bullet info for the parent
        Initialize(bulletVelocity, Constants.PLAYER_BASIC_BULLET_LIFETIME);

        //set sprite rotation
        float angle = Mathf.Atan2(bulletVelocity.y, bulletVelocity.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        int rand = Random.Range(0, 7);
        switch (rand)
        {
            case 0:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletGlassImpact);
                break;
            case 1:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact1);
                break;
            case 2:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact2);
                break;
            case 3:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact3);
                break;
            case 4:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact4);
                break;
            case 5:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact5);
                break;
            case 6:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletWoodImpact1);
                break;
            case 7:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletWoodImpact2);
                break;
            default:
                break;
        }

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Jeep])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.MotherShip])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Soldier])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Tank])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Zepplin])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyFastRocket])
            || collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemySlowRocket]))
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                int rand1 = Random.Range(0, 1);
                switch (rand1)
                {
                    case 0:
                        GameObject spark = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/Bullet_Ricochet_Sparks_1"), transform.position, Quaternion.identity);
                        spark.GetComponent<BulletRicochetSparksScript>().Initialize(collision.gameObject.GetComponent<Rigidbody2D>().velocity);
                        break;
                    case 1:
                        GameObject spark2 = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/Bullet_Ricochet_Sparks_2"), transform.position, Quaternion.identity);
                        spark2.GetComponent<BulletRicochetSparksScript>().Initialize(collision.gameObject.GetComponent<Rigidbody2D>().velocity);
                        break;
                    default:
                        break;
                }
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bird]))
        {
            Destroy(gameObject);
        }
    }
}
