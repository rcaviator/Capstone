using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    //adjustable variables
    //TODO: make these dynamic and get info from constants
    [SerializeField]
    bool scrolling;
    [SerializeField]
    bool parallax;
    [SerializeField]
    float backgroundSize;
    [SerializeField]
    float paralaxSpeed;

    //private variables
    Transform cameraTransform;
    Transform[] layers;
    float viewZone = 2;
    int leftIndex;
    int rightIndex;
    float lastCameraX;
    float lastCameraY;

    // Use this for initialization
    void Start ()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);

            float deltaY = cameraTransform.position.y - lastCameraY;
            transform.position += Vector3.up * (deltaY * paralaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            {
                ScrollLeft();
            }

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }
    }

    void ScrollLeft()
    {
        //int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, layers[rightIndex].position.y, layers[rightIndex].position.z);// * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    void ScrollRight()
    {
        //int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize, layers[rightIndex].position.y, layers[rightIndex].position.z);//Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
