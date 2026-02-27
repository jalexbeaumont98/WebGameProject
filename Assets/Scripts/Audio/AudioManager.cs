using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource SFXSourceOneShot;
    [SerializeField] private float defaultVolume;
    [SerializeField] private List<SoundEntry> soundLibrary; // This just looks nice in the inspector

    private Dictionary<SoundType, AudioClip> soundLibraryLookup;// Needed for the soundLibary (couldn't find a way around this)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Populate the dictionary with the values set in the inspector.
        soundLibraryLookup = new();
        foreach (SoundEntry entry in soundLibrary)
        {
            if (entry == null) continue;
            if (entry.soundType == SoundType.None || entry.audioClip == null) continue;

            if (!soundLibraryLookup.TryAdd(entry.soundType, entry.audioClip))
                Debug.LogWarning($"Duplicate SoundType mapping: {entry.soundType}", this);
        }
    }

    void Start()
    {
        // Plays the background music if it isn't already playing.
        if (!backgroundMusicSource.isPlaying) backgroundMusicSource.Play();
    }

    public void Play(SoundType soundType) => Play(soundType, defaultVolume);

    public void Play(SoundType soundType, float volume)
    {
        if (soundType == SoundType.None) return;
        if (!soundLibraryLookup.TryGetValue(soundType, out AudioClip audioClip) || audioClip == null) return;
        if (SFXSource.isPlaying && SFXSource.clip == audioClip) return;

        SFXSource.volume = volume;
        SFXSource.clip = audioClip;
        SFXSource.Play();
    }

    public void Stop()
    {
        if (SFXSource.clip == null)
        {
            Debug.LogWarning("Missing or invalid clip reference in SFXSource");
            return;
        }

        if (SFXSource.isPlaying) SFXSource.Stop();
        SFXSource.clip = null;
    }

    public void PlayOneShot(SoundType soundType) => PlayOneShot(soundType, defaultVolume);

    public void PlayOneShot(SoundType soundType, float volume)
    {
        if (soundType == SoundType.None) return;
        if (!soundLibraryLookup.TryGetValue(soundType, out AudioClip audioClip) || audioClip == null) return;

        SFXSourceOneShot.PlayOneShot(audioClip, volume);
    }
}
