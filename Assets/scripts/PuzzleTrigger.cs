using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject puzzleUI;
    public float activationDistance = 2.0f;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                puzzleUI.SetActive(true);
            }
        }
    }
}