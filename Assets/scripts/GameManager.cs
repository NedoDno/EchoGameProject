using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;
    public GameObject puzzleUI;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size = 4; // Setting the size of the puzzle grid
    private bool shuffling = false;
    private float gapThickness = 1f; // Adjusted gap thickness initialization

    private void Start()
    {
        puzzleUI.SetActive(false);
        pieces = new List<Transform>(); // Ensure the list is initialized before use
        CreateGamePieces(); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && puzzleUI.activeSelf)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform hitTransform = hit.transform;
                int index = pieces.IndexOf(hitTransform);
                if (index != -1)
                {
                    if (SwapIfValid(index, -size, size)) { return; }
                    if (SwapIfValid(index, +size, size)) { return; }
                    if (SwapIfValid(index, -1, 0)) { return; }
                    if (SwapIfValid(index, +1, size - 1)) { return; }
                }
            }
        }

        if (puzzleUI.activeSelf && !shuffling && CheckCompletion())
        {
            Debug.Log("Puzzle Solved!");
            ClosePuzzle();
        }
    }

    private void CreateGamePieces()
    {
        float width = 180f / size; // Updated to use the size for dynamic sizing
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  1 - (2 * width * row) - width,
                                                  0);
                piece.localScale = new Vector3((2 * width) - gapThickness, (2 * width) - gapThickness, 1); // Adjust scale uniformly
                piece.name = $"Piece {(row * size) + col}";

                if (row == size - 1 && col == size - 1)
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false); // Last piece is the empty space
                }
            }
        }
        Shuffle();
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        int newI = i + offset;
        if ((i % size) != colCheck && newI >= 0 && newI < pieces.Count && newI == emptyLocation)
        {
            SwapPieces(i, newI);
            return true;
        }
        return false;
    }

    private void SwapPieces(int i, int j)
    {
        (pieces[i].localPosition, pieces[j].localPosition) = (pieces[j].localPosition, pieces[i].localPosition);
        emptyLocation = i; // Update empty location
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"Piece {i}")
            {
                return false;
            }
        }
        return true;
    }

    public void ClosePuzzle()
    {
        puzzleUI.SetActive(false);
    }

    // Brute force shuffling.
    public void Shuffle()
    {
        if (!puzzleUI.activeSelf) return;
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            // Pick a random location.
            int rnd = Random.Range(0, size * size);
            // Only thing we forbid is undoing the last move.
            if (rnd == last) { continue; }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move.
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }
}
/*private void HandlePuzzleInteraction()
    {
        if (puzzleUI.activeSelf && Input.GetKeyDown(KeyCode.R) && refreshItemCount > 0)
        {
            refreshItemCount--;
            Shuffle();
        }
        if (puzzleUI.activeSelf && Input.GetKeyDown(KeyCode.S) && skipItemCount > 0)
        {
            skipItemCount--;
            CompletePuzzle();
        }
    }

public void StartPuzzle()
{
    SceneManager.LoadScene("test", LoadSceneMode.Additive);
    pieces = new List<Transform>();
    size = 4;
    CreateGamePieces(0.01f);
}
public void ExitPuzzle()
{
    isActive = false;
    SceneManager.UnloadScene("test");
}

public void ExitPuzzle()
{
    puzzle.SetActive(false);
}
}*/