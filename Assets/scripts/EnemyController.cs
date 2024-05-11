using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        InitializeSpawnPoints();
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

        float radius = 5f; 
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection.y = 0; 

        Vector3 spawnLocation = spawnPoint.transform.position + randomDirection;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(spawnLocation, out hit, radius, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position; 
            GameObject newEnemy = Instantiate(enemyPrefab, finalPosition, Quaternion.identity);
            currentEnemies++;

            NavMeshAgent agent = newEnemy.GetComponent<NavMeshAgent>();
            if (agent != null && !agent.isOnNavMesh)
            {

                if (!agent.Warp(finalPosition))
                {
                    //Debug.LogError("Failed to place agent on NavMesh. Destroying agent.");
                    //Destroy(newEnemy);
                }
            }
        }
        else
        {
            Debug.LogWarning("No valid NavMesh point found near spawn point!");
        }
    }




    public void RemoveSpawnPoint(GameObject spawnPointToRemove)
    {
        spawnPoints.Remove(spawnPointToRemove);
    }
}
