using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject thingToSpawn;
    public float spawnDelay;  //The amount of time before it first starts spawning
    public float timeInterval;  //The time between spawns
    //public int spawnNum;  //Number of thing to spawn in one go
    public int spawnLimit;  //The limit of total spawns it can do
    public bool destroyAfterSpawn;

    //public float percentHealthSpawn; //The amount of health it gets down to before it starts spawning
    //public Health hp;

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
        //if (hp && hp.currentHealth > hp.startingHealth*percentHealthSpawn)
        //    return;

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
        //for (int i=0; i<spawnNum; i++)
        //{
            spawnCount++;
            Instantiate(thingToSpawn, transform.position, Quaternion.identity);
        //}
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
