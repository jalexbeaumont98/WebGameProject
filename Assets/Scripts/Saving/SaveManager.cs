using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<UnitSpawner> spawnPoints = new List<UnitSpawner>();
    [SerializeField] private Transform movingPlatformParent;
    [SerializeField] private PlayerCameraController playerCamera;


    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    void Start()
    {
        if (playerCamera == null)
            playerCamera = FindFirstObjectByType<PlayerCameraController>();

        if (playerCamera == null)
        {
            Debug.LogWarning("No ThirdPersonCamera found in scene.");
            return;
        }

        if (GameManager.Instance.LoadGameFlag)
        {
            LoadGame();
            GameManager.Instance.LoadGameFlag = false; //quick and dirty way to enable loading
        }
    }
    public void SaveGame()
    {
        SaveData data = new SaveData();

        SavePlayer(data);
        SaveSpawnPoints(data);
        SaveMovingPlatforms(data);
        SaveCamera(data);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Saved game to: " + SavePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        LoadPlayer(data);
        LoadSpawnPoints(data);
        LoadMovingPlatforms(data);
        LoadCamera(data);

        Debug.Log("Loaded game from: " + SavePath);
    }

    private void SavePlayer(SaveData data)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player with tag 'Player' not found.");
            return;
        }

        PlayerHealthController health = player.GetComponent<PlayerHealthController>();

        data.player.position = new SerializableVector3(player.transform.position);

        if (health != null)
        {
            data.player.health = health.GetCurrentHealth();
        }
    }

    private void LoadPlayer(SaveData data)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player with tag 'Player' not found.");
            return;
        }

        player.transform.position = data.player.position.ToVector3();

        PlayerHealthController health = player.GetComponent<PlayerHealthController>();
        if (health != null)
        {
            // You need some way to restore health.
            // Replace this with whatever your real health script uses.
            health.SetCurrentHealth(data.player.health);
        }
    }

    private void SaveSpawnPoints(SaveData data)
    {
        foreach (UnitSpawner spawnPoint in spawnPoints)
        {
            if (spawnPoint == null)
                continue;

            SpawnPointSaveData spData = new SpawnPointSaveData();
            spData.spawnPointId = spawnPoint.SpawnPointId;
            spData.currentSpawnTimer = spawnPoint.GetCurrentSpawnTimer();

            for (int i = 0; i < spawnPoint.transform.childCount; i++)
            {
                Transform child = spawnPoint.transform.GetChild(i);

                EnemyHealth unitHealth = child.GetComponent<EnemyHealth>();
                if (unitHealth == null)
                    continue;

                UnitSaveData unitData = new UnitSaveData();
                unitData.position = new SerializableVector3(child.position);
                unitData.health = unitHealth.GetCurrentHealth();

                spData.aliveUnits.Add(unitData);
            }

            data.spawnPoints.Add(spData);
        }
    }

    private void LoadSpawnPoints(SaveData data)
    {
        Dictionary<string, SpawnPointSaveData> lookup = new Dictionary<string, SpawnPointSaveData>();
        foreach (SpawnPointSaveData spData in data.spawnPoints)
        {
            lookup[spData.spawnPointId] = spData;
        }

        foreach (UnitSpawner spawnPoint in spawnPoints)
        {
            if (spawnPoint == null)
                continue;

            if (!lookup.TryGetValue(spawnPoint.SpawnPointId, out SpawnPointSaveData spData))
                continue;

            spawnPoint.ClearSpawnedChildren();
            spawnPoint.SetCurrentSpawnTimer(spData.currentSpawnTimer);

            foreach (UnitSaveData unitData in spData.aliveUnits)
            {
                GameObject newUnit = spawnPoint.SpawnUnit();
                newUnit.transform.position = unitData.position.ToVector3();

                EnemyHealth health = newUnit.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    health.SetCurrentHealth(unitData.health);
                }
            }
        }
    }

    private void SaveMovingPlatforms(SaveData data)
    {
        if (movingPlatformParent == null)
            return;

        for (int i = 0; i < movingPlatformParent.childCount; i++)
        {
            Transform child = movingPlatformParent.GetChild(i);
            MovingPlatform platform = child.GetComponent<MovingPlatform>();

            if (platform == null)
                continue;

            PlatformSaveData platformData = new PlatformSaveData();
            platformData.platformId = platform.PlatformID;
            platformData.position = new SerializableVector3(platform.transform.position);
            platformData.nextWaypointIndex = platform.GetNextWaypointIndex();

            data.movingPlatforms.Add(platformData);
        }
    }

    private void LoadMovingPlatforms(SaveData data)
    {
        if (movingPlatformParent == null)
            return;

        Dictionary<string, PlatformSaveData> lookup = new Dictionary<string, PlatformSaveData>();
        foreach (PlatformSaveData pData in data.movingPlatforms)
        {
            lookup[pData.platformId] = pData;
        }

        for (int i = 0; i < movingPlatformParent.childCount; i++)
        {
            Transform child = movingPlatformParent.GetChild(i);
            MovingPlatform platform = child.GetComponent<MovingPlatform>();

            if (platform == null)
                continue;

            if (!lookup.TryGetValue(platform.PlatformID, out PlatformSaveData pData))
                continue;

            platform.SetPlatformState(
                pData.position.ToVector3(),
                pData.nextWaypointIndex
            );
        }
    }

    private void SaveCamera(SaveData data)
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player camera reference is missing.");
            return;
        }

        data.cameraData = playerCamera.GetCameraSaveData();
    }

    private void LoadCamera(SaveData data)
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player camera reference is missing.");
            return;
        }

        playerCamera.SetCameraSaveData(data.cameraData);
    }

    public bool HasSave()
    {
        return File.Exists(SavePath);
    }

    public void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save deleted.");
        }
    }
}