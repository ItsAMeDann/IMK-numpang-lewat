using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    [Range(0f, 1f)] public float spatialBlend = 0f;
    public bool loop;

    [HideInInspector] public AudioSource source;

}