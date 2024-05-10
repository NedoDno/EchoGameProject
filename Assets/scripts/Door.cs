using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openingHeight = 3f;  // How high the door should open
    public float openingSpeed = 1f;   // Speed of the door opening

    private bool isOpening = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = new Vector3(closedPosition.x, closedPosition.y + openingHeight, closedPosition.z);
    }

    void Update()
    {
        if (isOpening)
        {
            // Move the door upwards until it reaches the open position
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openingSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}
