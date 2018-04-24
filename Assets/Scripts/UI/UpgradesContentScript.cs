using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesContentScript : MonoBehaviour
{
    //get all button references
    GameObject apBulletsButton;
    GameObject hullButton;

	// Use this for initialization
	void Awake()
    {
        //instantiate buttons
        //ap button
        apBulletsButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/APUpgradeButton"), Vector3.zero, Quaternion.identity);
        apBulletsButton.transform.SetParent(null);
        apBulletsButton.transform.SetParent(gameObject.transform);
        apBulletsButton.GetComponent<RectTransform>().localScale = Vector3.one;

        //hull button
        hullButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/HullUpgradeButton"), Vector3.zero, Quaternion.identity);
        hullButton.transform.SetParent(null);
        hullButton.transform.SetParent(gameObject.transform);
        hullButton.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
