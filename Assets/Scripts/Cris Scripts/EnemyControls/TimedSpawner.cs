using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : Spawner {

    public int spawnNum; //Number of thing to spawn in one go

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void SpawnThing()
    {
        spawning = true;
        for (int i=0; i<spawnNum; i++)
        {
            //Eventually make it so that it just calls SpawnThing from spawner...
            if (this.spawnCount >= spawnLimit)
                return;
            spawnCount++;
            Instantiate(thingToSpawn, transform.position, Quaternion.identity);
        }
        spawning = false;
    }
}
