using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float masterVolume = 0f;
    public static AudioManager Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assigning the instance to the static property
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * masterVolume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.spread = s.spread;
        }
    }
   
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source != null)
        {
            s.source.Stop();
        }
    }

    // Function to set the master volume (called from a UI slider)
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;

        // Adjust the volume of all sounds based on the master volume
        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = s.volume * masterVolume;
            }
        }
    }

    public void SetAllAudioSourcesActive(bool isActive)
{
    foreach (Sound s in sounds)
    {
            s.source.enabled = isActive;
    }
}

}
