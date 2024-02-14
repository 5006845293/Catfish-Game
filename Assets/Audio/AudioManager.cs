using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip beginSplash;
    public AudioClip bubbles;
    public AudioClip buttonClick;
    public AudioClip pulloutSplash;
    public AudioClip whoosh1;
    public AudioClip whoosh2;
    public AudioClip whoosh3;
    public AudioClip whoosh4;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSource.PlayOneShot(background);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
