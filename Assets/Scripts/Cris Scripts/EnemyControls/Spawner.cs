using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject thingToSpawn;
    public int timeInterval;

    private float timer = 0;


    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            SpawnThing();
            timer = 0;
        }
    }

    private void SpawnThing()
    {
        Instantiate(thingToSpawn, transform.position, Quaternion.identity);
    }

}
