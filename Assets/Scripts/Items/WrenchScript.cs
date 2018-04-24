using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchScript : PauseableObject
{
    //bool to switch directions
    bool changeDirection = false;
    float directionTimer = 0f;
    float maxDirectionTimer = 0.25f;

    protected override void Awake()
    {
        base.Awake();

        transform.Rotate(new Vector3(0f, 0f, 45f));
    }


    private void Update()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //follow the player
            transform.position = new Vector3(GameManager.Instance.Player.transform.position.x + 1.5f, GameManager.Instance.Player.transform.position.y + 1.5f, GameManager.Instance.Player.transform.position.z);

            if (changeDirection)
            {
                if (directionTimer > 0)
                {
                    transform.Rotate(new Vector3(0, 0, Constants.FLIGHT_ENGINEER_WRENCH_ROTATION_RATE * Time.deltaTime));
                    directionTimer -= Time.deltaTime;
                }
                else
                {
                    changeDirection = false;
                }
            }
            else
            {
                if (directionTimer <= maxDirectionTimer)
                {
                    transform.Rotate(new Vector3(0, 0, -Constants.FLIGHT_ENGINEER_WRENCH_ROTATION_RATE * Time.deltaTime));
                    directionTimer += Time.deltaTime;
                }
                else
                {
                    changeDirection = true;
                }
            }
        }
    }
}
