using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {

    // Item to be dropped
    public GameObject drop;

    // Drop rate for item
    [Range(0, 100)]
    public int probability;
	
    // Chooses a random number between 0 and 100
    // If number is lower than set probability then spawns drop
	public void calculateDrop()
    {
        float rand = Random.Range(0, 100);

        if (rand <= probability && probability != 0)
            Instantiate(drop, transform.position, Quaternion.identity);
    }
}
