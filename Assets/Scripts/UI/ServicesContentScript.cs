using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesContentScript : MonoBehaviour
{
    //get service button references
    GameObject flightEngineerButton;
    GameObject repairPackButton;

    private void Awake()
    {
        //instantiate buttons
        //flight engineer button
        flightEngineerButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/FlightEngineerButton"), Vector3.zero, Quaternion.identity);
        flightEngineerButton.transform.SetParent(null);
        flightEngineerButton.transform.SetParent(gameObject.transform);
        flightEngineerButton.GetComponent<RectTransform>().localScale = Vector3.one;

        //repair pack button
        repairPackButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/RepairPackButton"), Vector3.zero, Quaternion.identity);
        repairPackButton.transform.SetParent(null);
        repairPackButton.transform.SetParent(gameObject.transform);
        repairPackButton.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
