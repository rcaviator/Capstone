using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesScript : PauseableObject
{

    float bulletLifetime = 0;
    float startTime = 0;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        //bullet lifetime
        if (!GameManager.Instance.Paused)
        {
            startTime += Time.deltaTime;
            if (startTime >= bulletLifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void Initialize(Vector2 velocity, float bulletTimer)
    {
        rBody.velocity = velocity;
        bulletLifetime = bulletTimer;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //put collision logic on each bullet

        //all bullets will die on ground collision
        if (collision.gameObject.tag == "Ground")
        {
            //Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
