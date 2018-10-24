using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip menuSelect;
    public AudioClip menuMove;
    public AudioClip menuBack;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuSelect()
    {
        audioSource.clip = menuSelect;
        audioSource.Play();
    }

    public void PlayMenuMove()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = menuMove;
            audioSource.Play();
        }
    }

    public void PlayMenuBack()
    {
        audioSource.clip = menuBack;
        audioSource.Play();
    }
}
