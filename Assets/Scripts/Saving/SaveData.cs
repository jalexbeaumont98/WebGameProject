using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerSaveData player = new PlayerSaveData();
    public List<SpawnPointSaveData> spawnPoints = new List<SpawnPointSaveData>();
    public List<PlatformSaveData> movingPlatforms = new List<PlatformSaveData>();
    public CameraSaveData cameraData = new CameraSaveData();
}

[Serializable]
public class PlayerSaveData
{
    public SerializableVector3 position;
    public int health;
}

[Serializable]
public class CameraSaveData
{
    public float yaw;
    public float currentPitch;
    public float distance;
    public float height;
}

[Serializable]
public class SpawnPointSaveData
{
    public string spawnPointId;
    public float currentSpawnTimer;
    public List<UnitSaveData> aliveUnits = new List<UnitSaveData>();
}

[Serializable]
public class UnitSaveData
{
    public SerializableVector3 position;
    public int health;
}

[Serializable]
public class PlatformSaveData
{
    public string platformId;
    public SerializableVector3 position;
    public int nextWaypointIndex;
}

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SerializableVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}