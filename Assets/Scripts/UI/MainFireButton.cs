using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainFireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPlayerFireButtonPress()
    {
        //Debug.Log("firing");
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("down");
        GameManager.Instance.Player.InputPlayerShoot = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Debug.Log("up");
        GameManager.Instance.Player.InputPlayerShoot = false;
    }
}
