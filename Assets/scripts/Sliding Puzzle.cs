using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class SlidingPuzzle : MonoBehaviour
{
    public Texture2D puzzleImage;
    public int gridSize = 4;
    private GameObject[,] tiles;
    private int[,] tilePositions; // Array to hold the correct positions for comparison
    private int emptyTileX;
    private int emptyTileY;
    public float tileSize = 100;
    private RectTransform panel;
    public bool PuzzleCompleted = false;
    public TextMeshProUGUI completionText;

    void Start()
    {
        //CreatePuzzleGrid();
        //ShuffleTiles();
        //PositionPanel();
        completionText.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PuzzleCompleted)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Piece"))
                {
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, null, out localPoint);
                    int x = Mathf.FloorToInt((localPoint.x + gridSize * tileSize * 0.5f) / tileSize);
                    int y = Mathf.FloorToInt((-localPoint.y + gridSize * tileSize * 0.5f) / tileSize);

                    if (x >= 0 && y >= 0 && x < gridSize && y < gridSize)
                    {
                        TryMoveTile(x, y);
                        if (CheckCompletion())
                        {
                            PuzzleCompleted = true;
                            completionText.enabled = true;
                            panel.gameObject.SetActive(false);
                        }
                    }
                    break;
                }
            }
        }
    }

    public void SetPuzzleImage(Texture2D image)
    {
        puzzleImage = image;
        CreatePuzzleGrid();
        ShuffleTiles();
        PositionPanel();
    }

    bool CheckCompletion()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (tilePositions[x, y] != (y * gridSize + x))
                {
                    return false;
                }
            }
        }
        return true;
    }

    void CreatePuzzleGrid()
    {
        tiles = new GameObject[gridSize, gridSize];
        tilePositions = new int[gridSize, gridSize]; // Initialize the positions array
        int tileWidth = puzzleImage.width / gridSize;
        int tileHeight = puzzleImage.height / gridSize;

        panel = new GameObject("Panel").AddComponent<RectTransform>();
        panel.SetParent(transform, false);
        panel.sizeDelta = new Vector2(gridSize * tileSize, gridSize * tileSize);
        panel.localPosition = Vector3.zero;
        panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(0.5f, 0.5f);

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                tilePositions[x, y] = y * gridSize + x; // Assign each tile a unique position ID based on its starting position

                if (x == gridSize - 1 && y == gridSize - 1)
                {
                    emptyTileX = x;
                    emptyTileY = y;
                    continue; // Skip creating the last tile to make an empty space
                }

                GameObject tile = new GameObject($"Tile_{x}_{y}", typeof(Image));
                tile.transform.SetParent(panel.transform, false);
                RectTransform rectTransform = tile.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(tileSize, tileSize);
                rectTransform.anchoredPosition = new Vector2(x * tileSize - gridSize * tileSize / 2 + tileSize / 2, -y * tileSize + gridSize * tileSize / 2 - tileSize / 2);
                tile.GetComponent<Image>().sprite = Sprite.Create(puzzleImage, new Rect(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Vector2(0.5f, 0.5f));
                tile.tag = "Piece";

                tiles[x, y] = tile;
            }
        }
    }

    void TryMoveTile(int x, int y)
    {
        if ((Mathf.Abs(emptyTileX - x) == 1 && emptyTileY == y) || (Mathf.Abs(emptyTileY - y) == 1 && emptyTileX == x))
        {
            MoveTile(x, y);
        }
    }

    void MoveTile(int x, int y)
    {
        GameObject tileToMove = tiles[x, y];
        tiles[emptyTileX, emptyTileY] = tileToMove;
        tiles[x, y] = null;

        // Update the tile positions array to reflect the new positions
        tilePositions[emptyTileX, emptyTileY] = tilePositions[x, y];
        tilePositions[x, y] = gridSize * gridSize - 1; // Set the new empty position

        tileToMove.GetComponent<RectTransform>().anchoredPosition = new Vector2(emptyTileX * tileSize - gridSize * tileSize / 2 + tileSize / 2, -emptyTileY * tileSize + gridSize * tileSize / 2 - tileSize / 2);
        emptyTileX = x;
        emptyTileY = y;
    }

    public void ShuffleTiles()
    {   
        if (!PuzzleCompleted)
        {
            for (int i = 0; i < 100; i++)
            {
                List<Vector2Int> validMoves = GetValidMoves();
                Vector2Int move = validMoves[Random.Range(0, validMoves.Count)];
                MoveTile(move.x, move.y);
            }
        }
    }

    List<Vector2Int> GetValidMoves()
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        if (emptyTileX > 0) validMoves.Add(new Vector2Int(emptyTileX - 1, emptyTileY));
        if (emptyTileX < gridSize - 1) validMoves.Add(new Vector2Int(emptyTileX + 1, emptyTileY));
        if (emptyTileY > 0) validMoves.Add(new Vector2Int(emptyTileX, emptyTileY - 1));
        if (emptyTileY < gridSize - 1) validMoves.Add(new Vector2Int(emptyTileX, emptyTileY + 1));
        return validMoves;
    }

    void PositionPanel()
    {
        panel.localPosition = Vector3.zero;
        panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(0.5f, 0.5f);
    }

    public void SkipPuzzle()
    {
        PuzzleCompleted = true;
        completionText.enabled = true;
        panel.gameObject.SetActive(false);
    }

    public void LeavePuzzle()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);

    }

}
