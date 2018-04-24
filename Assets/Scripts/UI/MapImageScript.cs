using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapImageScript : MonoBehaviour
{
    //map image reference
    Image mapImage;

    private void Start()
    {
        mapImage = GetComponent<Image>();

        mapImage.sprite = GameManager.Instance.Airports[GameManager.Instance.Level].Map;
    }
}
