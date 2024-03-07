using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //call AudioManager.instance.[FUNCTION] in desired part
    public static AudioManager instance; //singleton bc there is only one at a time
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource background;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, float volume)
    {
        //spawn game object
        AudioSource audioSource = Instantiate(soundEffect);
        //assign audioclip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of clip
        float soundLength = audioSource.clip.length;
        //destroy after play
        Destroy(audioSource.gameObject, soundLength);
    }

    public void PlayRandomSound(AudioClip[] randomClips, float volume)
    {
        int rand = Random.Range(0, randomClips.Length);
        
        //spawn game object
        AudioSource audioSource = Instantiate(soundEffect);
        //assign audioclips to randomize
        audioSource.clip = randomClips[rand];
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of clip
        float soundLength = audioSource.clip.length;
        //destroy after play
        Destroy(audioSource.gameObject, soundLength);
    }
}
