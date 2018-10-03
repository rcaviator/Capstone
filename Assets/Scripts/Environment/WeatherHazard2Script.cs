using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHazard2Script : PauseableObject
{
    //list for game object targets
    List<GameObject> targets;

    //lightning timer
    float timer = Constants.WEATHER_HAZARD_2_LIGHTNING_TIMER;

    //boolean for playing thunder if player is in range
    bool playerInRange = false;

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
            //strike targets if in game level
            if (MySceneManager.Instance.CurrentScene == Scenes.GameLevel)
            {   
                //if the timer is ready and there are targets
                if (timer >= Constants.WEATHER_HAZARD_2_LIGHTNING_TIMER && targets.Count > 0)
                {
                    //remove null entries and inactive bomber targets
                    targets.RemoveAll(item => item == null);
                    targets.RemoveAll(item => item.GetComponent<SpriteRenderer>().enabled == false);

                    //check if count is still greater than 0 after target cleanup
                    if (targets.Count > 0)
                    {
                        //select a random target
                        int randTarget = Random.Range(0, targets.Count);

                        //target aquired, create lightning bolt
                        GameObject lightning = Instantiate(ResourceManager.Instance.GetPrefab(Prefabs.LightnightBolt), transform.position, Quaternion.identity);

                        //face and angle the target
                        Vector3 centerPos = (transform.position + targets[randTarget].transform.position) / 2f;
                        lightning.transform.position = centerPos;
                        Vector3 direction = targets[randTarget].transform.position - transform.position;
                        direction = Vector3.Normalize(direction);
                        lightning.transform.right = direction;
                        Vector3 scale = Vector3.one;
                        scale.x = Vector3.Distance(transform.position, targets[randTarget].transform.position);
                        lightning.transform.localScale = scale;

                        if (playerInRange)
                        {
                            //play thunder sound
                            AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Thunder);
                        }

                        //reset timer
                        timer = 0f;
                    }
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
        //enter only applicable objects into the list
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Jeep]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Soldier]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Tank]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Zepplin]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bird]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemyFastRocket]) ||
            collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.EnemySlowRocket]))
        {
            if (!targets.Contains(collision.gameObject))
            {
                targets.Add(collision.gameObject);

                //toggle sound boolean if player is in range
                if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
                {
                    playerInRange = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targets.Contains(collision.gameObject))
        {
            targets.Remove(collision.gameObject);

            //toggle sound boolean if player is in range
            if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
            {
                playerInRange = false;
            }
        }
    }
}
