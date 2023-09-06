using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionMenuController : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider soundSlider;
    public Slider brightnessSlider;
    public Light directionalLight;
    public AudioManager audioManager;
    public List<AudioSource> audioSources;
    public AudioSource selectedAudioSource;
    private void Start()
    {
        // Add listener functions to respond to slider changes
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(AdjustSFX);
        }
        if (soundSlider != null)
        {
            soundSlider.onValueChanged.AddListener(AdjustSound);
        }
        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
        }

        // Deactivate all audio sources except the selected one
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.enabled = (audioSource == selectedAudioSource);
            }
        }
    }

    // Function to adjust sound volume based on the sfx slider value
    private void AdjustSFX(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetMasterVolume(volume);
            
        }
    }

    // Function to adjust sound volume based on the sound slider value
    private void AdjustSound(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }

    // Function to adjust brightness based on the brightness slider value
    private void AdjustBrightness(float brightness)
    {
        // Adjust the intensity of the directional light to control Skybox exposure
        directionalLight.intensity = brightness;

    }

    public void EnableAudioSources()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.enabled = true;
            }
        }
    }
}
