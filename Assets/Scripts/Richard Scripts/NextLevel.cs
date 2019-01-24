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
    }

    // Final "portal" detection
    // If player enters the portal, next level is loaded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            GameManager.gm.LoadNextLevel();
        }
    }
}
