using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBulletScript : ProjectilesScript
{
    float bulletLifetime = 0;
    float startTime = 0;


    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        
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


    }
}
