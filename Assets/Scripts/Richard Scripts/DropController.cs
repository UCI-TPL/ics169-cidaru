using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {
    public GameObject drop;

    [Range(0, 100)]
    public int probability;
	
	public void calculateDrop()
    {
        float rand = Random.Range(0, 100);

        if (rand <= probability && probability != 0)
            Instantiate(drop, transform.position, Quaternion.identity);
    }
}
