using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool isCarried = false;
    private Transform playerTransform;
    private float ogheight;

    //[SerializeField] private GameObject lamp;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        ogheight = transform.position.y;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) 
        {
            if (!isCarried)
            {
                PickUp();
            }
            else
            {
                PutDown();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void PickUp()
    {
        
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        Transform playerHand = GameObject.FindGameObjectWithTag("Player").transform.Find("HandPosition");
        transform.SetParent(playerHand);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isCarried = true;
    }
    void PutDown()
    {
        isCarried = false;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

       

        transform.position = new Vector3(transform.parent.position.x, ogheight, transform.parent.position.z);
        transform.SetParent(null);
    }
}
