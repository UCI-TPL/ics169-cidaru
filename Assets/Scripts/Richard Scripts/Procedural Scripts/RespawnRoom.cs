using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RespawnRoom : MonoBehaviour
{
    public bool roomFound = false;

    [HideInInspector]
    public bool isCopyRoom = false;

    private GameObject room;
    
    [HideInInspector]
    public GameObject copyRoom;

    private GameObject grid;

    // Room templates that store the information involving the type of rooms necessary
    private RoomTemplates templates;

    // Use this for initialization
    void Start()
    {
        // Finds the room template
        templates = GameManager.gm.GetComponent<RoomTemplates>();

        grid = GameObject.Find("Grid");

        room = gameObject;

        if (!isCopyRoom)
        {
            copyRoom = Instantiate(room, transform.position, Quaternion.identity);
            copyRoom.GetComponent<RespawnRoom>().isCopyRoom = true;

            RoomSpawner[] roomSpawners = copyRoom.transform.GetComponentsInChildren<RoomSpawner>();

            foreach (RoomSpawner roomSpawner in roomSpawners)
                Destroy(roomSpawner.gameObject);

            copyRoom.transform.parent = grid.transform;

            templates.copyRespawners.Add(copyRoom.GetComponent<RespawnRoom>());

            copyRoom.GetComponent<RespawnRoom>().roomFound = roomFound;

            copyRoom.SetActive(false);

            templates.respawners.Add(this);
        }
    }

    public void Respawn()
    {
        copyRoom.SetActive(true);

        RespawnRoom cRespawner = copyRoom.GetComponent<RespawnRoom>();

        cRespawner.room = cRespawner.gameObject;

        cRespawner.copyRoom = Instantiate(cRespawner.room, copyRoom.transform.position, Quaternion.identity);
        cRespawner.isCopyRoom = true;

        RoomSpawner[] roomSpawners = cRespawner.copyRoom.transform.GetComponentsInChildren<RoomSpawner>();

        foreach (RoomSpawner roomSpawner in roomSpawners)
            Destroy(roomSpawner.gameObject);

        cRespawner.copyRoom.transform.parent = grid.transform;

        templates.nextInLineRespawners.Add(cRespawner.copyRoom.GetComponent<RespawnRoom>());

        cRespawner.copyRoom.SetActive(false);

        Destroy(gameObject);
    }

    public void createCopy()
    {
        RespawnRoom cRespawner = GetComponent<RespawnRoom>();

        cRespawner.room = cRespawner.gameObject;

        cRespawner.copyRoom = Instantiate(cRespawner.room, transform.position, Quaternion.identity);
        cRespawner.isCopyRoom = true;

        RoomSpawner[] roomSpawners = cRespawner.copyRoom.transform.GetComponentsInChildren<RoomSpawner>();

        foreach (RoomSpawner roomSpawner in roomSpawners)
            Destroy(roomSpawner.gameObject);

        cRespawner.copyRoom.transform.parent = grid.transform;

        templates.nextInLineRespawners.Add(cRespawner.copyRoom.GetComponent<RespawnRoom>());

        cRespawner.copyRoom.SetActive(false);
    }
}
