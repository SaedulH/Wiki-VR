using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField]
    private StringSO SO;

    private AudioSource Music;

    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Music = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        Music.volume = (0.5F * SO.Volume * 0.1F);
    }

}
