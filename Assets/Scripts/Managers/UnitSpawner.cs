using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject unitPrefab;
    public Transform spawnPoint;

    [Header("Timing")]
    public float startSpawnInterval = 10f;   // starting time between spawns
    public float minSpawnInterval = 5f;   // hard cap (fastest possible)
    public float spawnAcceleration = 0.2f;  // how much faster it gets each spawn

    private float currentInterval;
    private float timer;

    void Start()
    {
        currentInterval = startSpawnInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            SpawnUnit();
            timer = 0f;

            // Increase spawn rate (decrease interval)
            currentInterval -= spawnAcceleration;

            // Clamp to hard cap
            if (currentInterval < minSpawnInterval)
                currentInterval = minSpawnInterval;
        }
    }

    void SpawnUnit()
    {
        Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
    }
}
