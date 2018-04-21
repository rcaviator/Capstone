//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PauseableObject : MonoBehaviour
{
    //component references
    protected Rigidbody2D rBody;
    protected Animator anim;

    //rigidbody references
    float gravity;
    Vector2 storedVelocity;
    bool isSimulated;

    protected virtual void Awake()
    {
        //set references
        if (GetComponent<Rigidbody2D>())
        {
            rBody = GetComponent<Rigidbody2D>();
        }

        if (GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }

        //set reference in GameManager
        GameManager.Instance.AddPauseableObject(this);
    }

    //private void Update()
    //{

    //}

    private void OnDestroy()
    {
        //remove reference in GameManger
        GameManager.Instance.RemovePauseableObject(this);
    }

    /// <summary>
    /// Pauses the object
    /// </summary>
    public void PauseObject()
    {
        if (rBody)
        {
            //set references only if applicable
            if (rBody.bodyType != RigidbodyType2D.Static)
            {
                //gather references and then disable simulated
                gravity = rBody.gravityScale;
                storedVelocity = rBody.velocity;
                isSimulated = rBody.simulated;

                rBody.gravityScale = 0f;
                rBody.velocity = Vector2.zero;
                rBody.simulated = false;
            }
        }

        if (anim)
        {
            anim.speed = 0f;
        }
    }

    /// <summary>
    /// Unpauses the object
    /// </summary>
    public void UnPauseObject()
    {
        if (rBody)
        {
            //apply references only if applicable
            if (rBody.bodyType != RigidbodyType2D.Static)
            {
                //enable simulated and then apply references
                rBody.simulated = isSimulated;
                rBody.gravityScale = gravity;
                rBody.velocity = storedVelocity;
            }
        }

        if (anim)
        {
            anim.speed = 1f;
        }
    }
}
