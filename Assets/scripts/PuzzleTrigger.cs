using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject puzzle;
    public float activationDistance = 2.0f;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 0;
                puzzle.SetActive(true);

            }
        }
    }
}