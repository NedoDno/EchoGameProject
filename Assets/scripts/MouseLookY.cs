using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseLookY : MonoBehaviour
{
    public float sensitivityVert = 3.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    private float vertialRot = 0;
    // Update is called once per frame
    void Update()
    {
        vertialRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
        vertialRot = Mathf.Clamp(vertialRot, minimumVert, maximumVert);
        float horizontalRot = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(vertialRot, horizontalRot, 0);
    }
}