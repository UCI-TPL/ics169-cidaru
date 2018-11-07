using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : Bomb {
    public GameObject bomb;

	// Update is called once per frame
	void Update () {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
	}
}
