using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiedTrojanSFX : MonoBehaviour
{
    public SFXStorage audioClips;

    private AudioClip chosenSFX;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        chosenSFX = audioClips.soundEffs[Random.Range(0, audioClips.soundEffs.Count)];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = chosenSFX;
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.Play();

        Destroy(gameObject, 1.5f);
    }
}
