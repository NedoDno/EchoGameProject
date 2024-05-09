using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject player;
    public float activationDistance = 2.0f;
    public GameObject puzzle;  
    public Texture2D[] puzzleImages;
    public GameObject canvas;
    private GameObject minigameInstance;
    private bool isPuzzleActive = false;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (minigameInstance == null) 
                {
                    minigameInstance = Instantiate(puzzle, Vector3.zero, Quaternion.identity);
                    minigameInstance.transform.SetParent(canvas.transform, false);
                    minigameInstance.SetActive(false); 

                    int imageIndex = Random.Range(0, puzzleImages.Length);
                    SlidingPuzzle slidingPuzzle = minigameInstance.GetComponent<SlidingPuzzle>();
                    if (slidingPuzzle != null)
                    {
                        slidingPuzzle.SetPuzzleImage(puzzleImages[imageIndex]);
                    }
                }

                minigameInstance.SetActive(true);
                Time.timeScale = 0;  
            }
        }
        if (minigameInstance != null)
        {
            SlidingPuzzle slidingPuzzle = minigameInstance.GetComponent<SlidingPuzzle>();
            if (slidingPuzzle != null && slidingPuzzle.PuzzleCompleted)
            {
                Debug.Log("Puzzle Completed!");
            }
        }
    }
}