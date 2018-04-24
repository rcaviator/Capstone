using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsContentScript : MonoBehaviour
{
    //get weapon and defense button references
    GameObject shieldButton;
    GameObject clusterBombButton;
    GameObject seekerMissileButton;

    private void Awake()
    {
        //instantiate buttons
        //shield button
        shieldButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/ShieldButton"), Vector3.zero, Quaternion.identity);
        shieldButton.transform.SetParent(null);
        shieldButton.transform.SetParent(gameObject.transform);
        shieldButton.GetComponent<RectTransform>().localScale = Vector3.one;

        //clusterbomb button
        clusterBombButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/ClusterBombButton"), Vector3.zero, Quaternion.identity);
        clusterBombButton.transform.SetParent(null);
        clusterBombButton.transform.SetParent(gameObject.transform);
        clusterBombButton.GetComponent<RectTransform>().localScale = Vector3.one;

        //seeker missile button
        seekerMissileButton = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PreLevel menu/Buttons/SeekerMissileButton"), Vector3.zero, Quaternion.identity);
        seekerMissileButton.transform.SetParent(null);
        seekerMissileButton.transform.SetParent(gameObject.transform);
        seekerMissileButton.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
