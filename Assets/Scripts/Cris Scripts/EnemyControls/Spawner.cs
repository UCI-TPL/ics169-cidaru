using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject thingToSpawn;
    public float spawnDelay;  //The amount of time before it first starts spawning
    public float timeInterval;  //The time between spawns
    public int spawnLimit;  //The limit of total spawns it can do
    public bool destroyAfterSpawn;

    [HideInInspector]
    public bool spawning;

    protected float totalTimer;
    protected float timer;
    protected float spawnCount;

    private void Start()
    {
        totalTimer = 0;
        timer = timeInterval;
        spawnCount = 0;
        spawning = false;
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

    protected virtual void SpawnThing()
    {
        spawning = true;
        if (spawnCount >= spawnLimit)
            return;
        spawnCount++;
        Instantiate(thingToSpawn, transform.position, Quaternion.identity);
        spawning = false;
    }

    private void checkLimit()
    {
        if (destroyAfterSpawn && spawnCount >= spawnLimit)
            Destroy(gameObject);
    }

    public void reset()
    {
        totalTimer = 0;
        timer = timeInterval;
        spawnCount = 0;
    }
}
