using UnityEngine;

[System.Serializable]
public class SoundEntry
{
    public SoundType soundType; // Stores the enum sound type as the key
    public AudioClip audioClip; // Store the actual audio clip
}
