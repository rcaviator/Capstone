using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRicochetSparksScript : PauseableObject
{
    float timer = 0f;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();

        //AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BulletMetalImpact1);
	}

    /// <summary>
    /// Matches velocity of collided object
    /// </summary>
    /// <param name="vel">the velocity of the collided object</param>
    public void Initialize(Vector2 vel)
    {
        rBody.velocity = vel;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= Constants.BULLET_RICOCHET_SPARKS_LIFETIME)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
