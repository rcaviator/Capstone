using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBulletScript : ProjectilesScript
{
    //float bulletLifetime = 0;
    //float startTime = 0;


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
        ////create new vectory with velocity to the mouse plus camera velocity
        //Vector2 velocity = GameManager.Instance.Reticle.transform.position - transform.position;
        //velocity.Normalize();
        //velocity *= Constants.PLAYER_BASIC_BULLET_ATTACK_LIFETIME;
        bulletVelocity.Normalize();
        bulletVelocity *= Constants.PLAYER_BASIC_BULLET_SPEED;

        Initialize(bulletVelocity, Constants.PLAYER_BASIC_BULLET_ATTACK_LIFETIME);

        float angle = Mathf.Atan2(bulletVelocity.y, bulletVelocity.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
        //AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyProjectile);
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

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                int rand1 = Random.Range(0, 1);
                switch (rand1)
                {
                    case 0:
                        GameObject spark = Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Bullet_Ricochet_Sparks_1"), transform.position, Quaternion.identity);
                        spark.GetComponent<BulletRicochetSparksScript>().Initialize(collision.gameObject.GetComponent<Rigidbody2D>().velocity);
                        break;
                    case 1:
                        GameObject spark2 = Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Bullet_Ricochet_Sparks_2"), transform.position, Quaternion.identity);
                        spark2.GetComponent<BulletRicochetSparksScript>().Initialize(collision.gameObject.GetComponent<Rigidbody2D>().velocity);
                        break;
                    default:
                        break;
                }
            }

            Destroy(gameObject);
        }
        
    }
}
