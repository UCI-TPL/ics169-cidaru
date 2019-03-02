using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
    private bool active = false;
    private AudioSource audioSource;

    public void Awake()
    {
        active = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        active = true;
        audioSource.Play();
        GetComponent<Animator>().SetBool("open", true);
    }

    public bool isActive()
    {
        return active;
    }

    // Final "portal" detection
    // If player enters the portal, next level is loaded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            if (PlayerPrefs.GetInt("Level") == 2)
                GameManager.gm.LoadNextLevel();
            else if (PlayerPrefs.GetInt("Level") == 5)
                GameManager.gm.LoadBossLevel();
            else
                GameManager.gm.ReloadLevel();
        }
    }
}
