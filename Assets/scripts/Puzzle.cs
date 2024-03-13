using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;
    public void Operate()
    {
        Debug.Log("Open Puzzle");
        puzzle.SetActive(true);
        
    }
    
}
