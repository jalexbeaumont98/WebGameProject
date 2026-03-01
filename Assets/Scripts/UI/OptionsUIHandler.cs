using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUIHandler : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private float defaultMusicVolume = 0.5f;
    [SerializeField] private float defaultSFXVolume = 0.7f;

    void Start()
    {
        ResetAudioSlidersToDefault();
    //    SetVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 90));
    }

    // public void SetVolume(float volume)
    // {
        // if (volume < 0.0015) volume = defaultVolume;

        // ResetMusicSlider(volume);
        // PlayerPrefs.SetFloat("SavedMusicVolume", volume);
        // audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume/100) *20f);
    // }

    public void ResetAudioSlidersToDefault()
    {
        UpdateSFXSlider(defaultSFXVolume);
        UpdateMusicSlider(defaultMusicVolume);

        SetSFXVolume(defaultSFXVolume);
        SetMusicVolume(defaultMusicVolume);
    }

    // Referenced by slider to set sfx volume
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) *20f);
    }

    // Referenced by slider to set music volume
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) *20f);
    }

    // Updates the position of the sfx slider to match actual volume.
    // Used for resetting volume to defaults.
    public void UpdateSFXSlider(float value)
    {
        sfxSlider.value = value;
    }

    // Updates the position of the music slider to match actual volume.
    // Used for resetting volume to defaults.
    public void UpdateMusicSlider(float value)
    {
        musicSlider.value = value;
    }
}
