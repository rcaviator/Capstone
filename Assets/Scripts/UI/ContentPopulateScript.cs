using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentPopulateScript : MonoBehaviour
{
    GameObject[] enemyPrefabs;
    GameObject[] environmentPrefabs;
    GameObject[] utilityPrefabs;
    GameObject objectButtonPrefab;
    List<GameObject> prefabs;

	// Use this for initialization
	void Awake ()
    {
        //load prefabs
        enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemies/");
        environmentPrefabs = Resources.LoadAll<GameObject>("Prefabs/Environment/");
        utilityPrefabs = Resources.LoadAll<GameObject>("Prefabs/Utility/");

        //load button
        objectButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/Level editor/SpawnableObjectButton");

        //create and initialize list
        prefabs = new List<GameObject>();
        prefabs.AddRange(enemyPrefabs);
        prefabs.AddRange(environmentPrefabs);
        prefabs.AddRange(utilityPrefabs);

        //instantiate each button object, set its parent, and set its game object
        foreach (GameObject item in prefabs)
        {
            GameObject ob = Instantiate(objectButtonPrefab);
            ob.transform.SetParent(null);
            ob.transform.SetParent(gameObject.transform);
            //fix scaling
            ob.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            ob.GetComponent<SpawnableObjectButtonScript>().SetGameObject(item);
        }

        ////set the ui to the first object
        //foreach (GameObject item in prefabs)
        //{
        //    if (item.GetComponent<SpawnableObjectButtonScript>())
        //    {
        //        item.GetComponent<SpawnableObjectButtonScript>().OnSelectObjectClick();
        //        break;
        //    }
        //}
	}
}
