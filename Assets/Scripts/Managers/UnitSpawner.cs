using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject unitPrefab;
    public Transform spawnPoint;

    [Header("Save ID")]
    [SerializeField] private string spawnPointId;
    public string SpawnPointId => spawnPointId;


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

    public GameObject SpawnUnit()
    {
        GameObject newEnemy = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
        return newEnemy;
    }
    public void ClearSpawnedChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public float GetCurrentSpawnTimer()
    {
        return currentInterval;
    }

    public void SetCurrentSpawnTimer(float value)
    {
        currentInterval = value;
    }
}
