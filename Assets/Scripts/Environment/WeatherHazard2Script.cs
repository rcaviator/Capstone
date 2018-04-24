using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHazard2Script : PauseableObject
{
    //list for game object targets
    List<GameObject> targets;

    //lightning timer
    float timer = Constants.WEATHER_HAZARD_2_LIGHTNING_TIMER;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        targets = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //process if not paused
        if (!GameManager.Instance.Paused)
        {
            //if theres any targets in range
            if (targets.Count > 0)
            {
                //if the timer is ready
                if (timer >= Constants.WEATHER_HAZARD_2_LIGHTNING_TIMER)
                {
                    //remove null entries, objects were destroyed elsewhere
                    targets.RemoveAll(item => item == null);

                    //select a random target
                    int randTarget = Random.Range(0, targets.Count);

                    //target aquired
                    Debug.Log("Fire lighting on " + targets[randTarget].name);

                    timer = 0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!targets.Contains(collision.gameObject))
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targets.Contains(collision.gameObject))
        {
            targets.Remove(collision.gameObject);
        }
    }
}
