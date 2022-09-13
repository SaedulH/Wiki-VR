using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0F, 1F)]
    public float volume;

    [Range(0F, 3F)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
