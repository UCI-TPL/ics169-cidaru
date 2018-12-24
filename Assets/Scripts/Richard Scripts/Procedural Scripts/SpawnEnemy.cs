using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject enemy;

    public void Awake()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
