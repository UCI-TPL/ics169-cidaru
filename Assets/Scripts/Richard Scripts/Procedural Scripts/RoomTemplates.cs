using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    [Header("Three Way Single Room")]
    public GameObject[] blrRooms;
    public GameObject[] tlbRooms;
    public GameObject[] tlrRooms;
    public GameObject[] trbRooms;

    [Header("Two Way Single Room")]
    public GameObject[] blRooms;
    public GameObject[] brRooms;
    public GameObject[] tlRooms;
    public GameObject[] trRooms;
    public GameObject[] tbRooms;
    public GameObject[] lrRooms;
    public GameObject[] tRooms;
    public GameObject[] bRooms;
    public GameObject[] lRooms;
    public GameObject[] rRooms;

    [Header("Two Way Double Room")]
    public GameObject[] doubleLRRooms;
    public GameObject[] doubleRLRooms;
    public GameObject[] doubleTBRooms;
    public GameObject[] doubleBTRooms;

    public int minRooms = 10;
    public int maxRooms = 30;

    [HideInInspector]
    public List<GameObject> rooms = new List<GameObject>();

    //[HideInInspector]
    public List<RespawnRoom> respawners = new List<RespawnRoom>();

    //[HideInInspector]
    public List<RespawnRoom> copyRespawners = new List<RespawnRoom>();

    //[HideInInspector]
    public List<RespawnRoom> nextInLineRespawners = new List<RespawnRoom>();

    public GameObject finalRoomObject;
    private bool spawned = false;

    private void Update()
    {
        if (!GameManager.gm.spawningRooms && !spawned)
        {
            Instantiate(finalRoomObject, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    public bool checkMinRooms()
    {
        return rooms.Count >= minRooms;
    }

    public bool checkMaxRooms()
    {
        return rooms.Count >= maxRooms;
    }
}
