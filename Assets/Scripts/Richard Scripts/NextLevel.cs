using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

    private bool active = false;

    public void Awake()
    {
        active = false;
    }

    public void Activate()
    {
        active = true;
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

            if (PlayerPrefs.GetInt("Level") == 3)
                GameManager.gm.LoadBossLevel();
            else
                GameManager.gm.LoadNextLevel();
        }
    }
}
