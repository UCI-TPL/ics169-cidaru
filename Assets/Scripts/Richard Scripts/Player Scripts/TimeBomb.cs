using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : Bomb {

    // Field to spawn after bomb timer is over
    public GameObject bomb;

	// Update is called once per frame
	void Update () {
        // Countdown the time bomb timer
        bombTimer -= Time.deltaTime;

        // If timer expired, then explode
        if (bombTimer <= 0f)
        {
            // Create time field area where the bomb was
            Instantiate(bomb, transform.position, Quaternion.identity);

            // Destroy the game object
            Destroy(gameObject);
        }
	}
}
