using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds; 
    
    public static AudioManager instance;
    void Awake()
    {   
        foreach (Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Play();
        Debug.Log("Playing "+name);
    }
}
