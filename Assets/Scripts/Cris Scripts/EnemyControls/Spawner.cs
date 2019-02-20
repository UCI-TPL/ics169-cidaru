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
    protected bool continuousSpawn;

    private void Start()
    {
        totalTimer = 0;
        timer = timeInterval;
        spawnCount = 0;
        spawning = false;
        continuousSpawn = true;
    }


    private void FixedUpdate()
    {
        if (continuousSpawn)
        {
            if (totalTimer <= spawnDelay)
            {
                totalTimer += Time.deltaTime;
                timer = timeInterval;
                return;
            }

            timer += Time.deltaTime;
            if (timer >= timeInterval)
            {
                if (spawnCount < spawnLimit)
                    StartCoroutine(SpawnThing());
                timer = 0;
            }

            checkLimit(); //Destroys gameObject if at or over limit
        }
    }

    public void Spawn()
    {
        if (continuousSpawn)
            timer = timeInterval;
        else if (spawnCount < spawnLimit)
                StartCoroutine(SpawnThing());
    }

    protected virtual IEnumerator SpawnThing()
    {
        spawning = true;
        spawnCount++;
        Instantiate(thingToSpawn, transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0);
        spawning = false;
    }

    private void checkLimit()
    {
        if (destroyAfterSpawn && spawnCount >= spawnLimit)
            Destroy(gameObject);
    }

    public void reset()
    {
        resetTimer();
        spawnCount = 0;
    }

    public void resetTimer()
    {
        totalTimer = 0;
        timer = timeInterval;
    }

    public void setContinuousSpawn(bool b)
    {
        continuousSpawn = b;
    }

    public float getTimer()
    {
        return timer;
    }
}
