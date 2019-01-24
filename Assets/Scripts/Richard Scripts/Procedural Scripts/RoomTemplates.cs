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

    [HideInInspector]
    public List<AddRoom> roomType = new List<AddRoom>();

    [HideInInspector]
    public List<RespawnRoom> respawners = new List<RespawnRoom>();

    [HideInInspector]
    public List<RespawnRoom> copyRespawners = new List<RespawnRoom>();

    [HideInInspector]
    public List<RespawnRoom> nextInLineRespawners = new List<RespawnRoom>();

    //public GameObject finalRoomObject;

    private GameObject grid;

    [Header("Portal Rooms")]
    public GameObject[] portalTRooms;
    public GameObject[] portalBRooms;
    public GameObject[] portalLRooms;
    public GameObject[] portalRRooms;
    public GameObject[] portalTLRooms;
    public GameObject[] portalBRRooms;
    public GameObject[] portalTRRooms;
    public GameObject[] portalBLRooms;

    private bool spawned = false;
    private int rand;
    private GameObject room;
    private GameObject copyRoom;

    public void Awake()
    {
        grid = GameObject.Find("Grid");

    }

    private void Update()
    {
        if (!GameManager.gm.spawningRooms && !spawned)
        {
            AddRoom.RoomType roomT = roomType[roomType.Count - 1].roomType;
            
            switch (roomT)
            {
                case AddRoom.RoomType.T:
                    rand = Random.Range(0, portalTRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalTRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.B:
                    rand = Random.Range(0, portalBRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalBRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);
                    break;

                case AddRoom.RoomType.L:
                    rand = Random.Range(0, portalLRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalLRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.R:
                    rand = Random.Range(0, portalRRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalRRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.TL:
                    rand = Random.Range(0, portalTLRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalTLRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.TR:
                    rand = Random.Range(0, portalTRRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalTRRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.BL:
                    rand = Random.Range(0, portalBLRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalBLRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;

                case AddRoom.RoomType.BR:
                    rand = Random.Range(0, portalBRRooms.Length);

                    respawners.RemoveAt(respawners.Count - 1);

                    // Destroys the old copy room
                    copyRoom = copyRespawners[copyRespawners.Count - 1].gameObject;
                    copyRespawners.RemoveAt(copyRespawners.Count - 1);
                    Destroy(copyRoom);

                    room = Instantiate(portalBRRooms[rand], rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;

                    Destroy(rooms[rooms.Count - 1]);

                    break;
            }

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
