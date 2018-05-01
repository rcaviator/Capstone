using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHazard3Script : PauseableObject
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

    }

    //// Update is called once per frame
    //void Update ()
    //   {
    //       //process if not paused
    //       if (!GameManager.Instance.Paused)
    //       {

    //       }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            collision.GetComponent<PlayerScript>().Downdraft = true;
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]))
        {
            collision.GetComponent<BomberScript>().Downdraft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Player]))
        {
            collision.GetComponent<PlayerScript>().Downdraft = false;
        }
        else if (collision.CompareTag(GameManager.Instance.GameObjectTags[Constants.Tags.Bomber]))
        {
            collision.GetComponent<BomberScript>().Downdraft = false;
        }
    }
}
