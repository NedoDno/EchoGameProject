using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject[] enemyPrefabs; 
    public float spawnInterval = 2f; 
    public int maxEnemies = 10;

    private int currentEnemies = 0; 
    private float timer = 0f;

    void Start()
    {
        InitializeSpawnPoints();
    }
    void Update()
    {
        if (currentEnemies < maxEnemies && timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        timer += Time.deltaTime; 
    }
    void InitializeSpawnPoints()
    {
        spawnPoints.Clear();

        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPoint in spawnPointObjects)
        {
            spawnPoints.Add(spawnPoint);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("All spawn points destroyed!");
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        GameObject spawnPoint = spawnPoints[spawnPointIndex];

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[enemyIndex];

        Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

        currentEnemies++;
    }
    public void RemoveSpawnPoint(GameObject spawnPointToRemove)
    {
        spawnPoints.Remove(spawnPointToRemove);
    }
}
