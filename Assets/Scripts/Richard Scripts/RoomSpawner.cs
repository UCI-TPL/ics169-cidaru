using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    public GameManager.Opening openingDirection;

    private RoomTemplates templates;
    private GameObject grid;
    private int rand;
    private bool spawned;

    private void Start()
    {
        templates = GameObject.Find("Game Manager").GetComponent<RoomTemplates>();
        grid = GameObject.Find("Grid");

        spawned = false;

        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (spawned)
        {
            return;
        }

        if (openingDirection == GameManager.Opening.Bottom)
        {
            rand = Random.Range(0, templates.bottomRooms.Length);

            GameObject room = Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Top)
        {
            rand = Random.Range(0, templates.topRooms.Length);

            GameObject room = Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Left)
        {
            rand = Random.Range(0, templates.leftRooms.Length);

            GameObject room = Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Right)
        {
            rand = Random.Range(0, templates.rightRooms.Length);

            GameObject room = Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }

        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn Point"))
        {
            Destroy(gameObject);
        }
    }
}
