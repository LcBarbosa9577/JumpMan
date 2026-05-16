using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Spawn")]
    public List<GameObject> obstaclePrefabs;
    public float startDelay = 2f;
    public float spawnInterval = 1f;
    public Transform spawnPoint;

    [Header("Game Over")]
    public bool IsGameOver = false;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
        }
       else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnInterval);
    }

    private void Update()
    {
        if (IsGameOver)
        {
            CancelInvoke(nameof(SpawnObstacle));
        }
    }

    void SpawnObstacle()
    {
        if (!IsGameOver && obstaclePrefabs.Count > 0)
        {
            int index = Random.Range(0, obstaclePrefabs.Count);
            GameObject obstacleChosed = obstaclePrefabs[index];

            Instantiate(obstacleChosed, spawnPoint.position, spawnPoint.rotation);
        }
    }

   

}
