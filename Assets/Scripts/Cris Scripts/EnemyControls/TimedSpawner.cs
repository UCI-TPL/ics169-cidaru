using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : Spawner {

    public int spawnNum; //Number of thing to spawn in one go
    public float prebirthWaitTime = 1f;
    public float postbirthWaitTime = 0f;

    protected override void SpawnThing()
    {
        ///Broken af
        spawning = true;
        for (int i = 0; i < spawnNum; i++)
        {
            spawnCount++;
            Instantiate(thingToSpawn, transform.position, Quaternion.identity);
        }
        spawning = false;
    }
}
