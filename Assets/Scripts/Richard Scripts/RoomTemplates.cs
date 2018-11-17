using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {
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

    public List<GameObject> rooms;

    public float waitTime;
    public GameObject finalRoomObject;
    private bool spawned = false;

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && !spawned)
        {
            Instantiate(finalRoomObject, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            spawned = true;
        }
    }
}
