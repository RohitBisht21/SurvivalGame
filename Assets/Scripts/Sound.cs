using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;
    [Range(1, 100)]
    public int spread;

    public bool loop;
    public bool playOnAwake;
    [HideInInspector]
    public AudioSource source;
}
