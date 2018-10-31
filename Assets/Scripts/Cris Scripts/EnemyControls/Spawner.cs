using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject thingToSpawn;
    public int spawnDelay;
    public int timeInterval;

    private float totalTimer = 0;
    private float timer;

    private void Start()
    {
        timer = timeInterval;
    }


    private void FixedUpdate()
    {
        if (totalTimer < spawnDelay)
        {
            totalTimer += Time.deltaTime;
            return;
        }

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
