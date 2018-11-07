using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject thingToSpawn;
    public int spawnDelay;
    public int timeInterval;
    public int spawnLimit;

    private float totalTimer;
    private float timer;
    private float spawnCount;

    private void Start()
    {
        totalTimer = 0;
        timer = timeInterval;
        spawnCount = 0;
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

        checkLimit(); //Destroys gameObject if at or over limit
    }

    private void SpawnThing()
    {
        if (spawnCount >= spawnLimit)
            return;
        spawnCount++;
        Instantiate(thingToSpawn, transform.position, Quaternion.identity);
    }

    private void checkLimit()
    {
        if (spawnCount >= spawnLimit)
            Destroy(gameObject);
    }
}
