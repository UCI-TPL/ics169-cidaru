using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    public GameManager.Opening openingDirection;

    private RoomTemplates templates;
    private GameObject grid;
    private int rand;
    private bool spawned;

    private int verticalDistance = 16;
    private int horizontalDistance = 20;

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
            List<GameObject> botRooms = new List<GameObject>();
            updateBotList(botRooms);

            rand = Random.Range(0, botRooms.Count - 1);

            GameObject room = Instantiate(botRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Top)
        {
            List<GameObject> topRooms = new List<GameObject>();
            updateTopList(topRooms);

            rand = Random.Range(0, topRooms.Count - 1);

            GameObject room = Instantiate(topRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Left)
        {
            List<GameObject> leftRooms = new List<GameObject>();
            updateLeftList(leftRooms);

            rand = Random.Range(0, leftRooms.Count - 1);

            GameObject room = Instantiate(leftRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Right)
        {
            List<GameObject> rightRooms = new List<GameObject>();
            updateRightList(rightRooms);

            rand = Random.Range(0, rightRooms.Count - 1);

            GameObject room = Instantiate(rightRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }

        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn Point"))
        {
            RoomSpawner otherRS = other.GetComponent<RoomSpawner>();

            if (otherRS == null)
            {
                Destroy(gameObject);
                spawned = true;
                return;
            }

            if (!otherRS.spawned && !spawned)
            {
                if (checkBL(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.blRooms.Length - 1);

                    GameObject room = Instantiate(templates.blRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkBR(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.brRooms.Length - 1);

                    GameObject room = Instantiate(templates.brRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTL(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.tlRooms.Length - 1);

                    GameObject room = Instantiate(templates.tlRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTR(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.trRooms.Length - 1);

                    GameObject room = Instantiate(templates.trRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
            }

            spawned = true;
        }
    }

    private bool checkBL(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Bottom && dir == GameManager.Opening.Left) ||
            (openingDirection == GameManager.Opening.Left && dir == GameManager.Opening.Bottom));
    }

    private bool checkBR(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Bottom && dir == GameManager.Opening.Right) ||
            (openingDirection == GameManager.Opening.Right && dir == GameManager.Opening.Bottom));
    }

    private bool checkTL(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Top && dir == GameManager.Opening.Left) ||
            (openingDirection == GameManager.Opening.Left && dir == GameManager.Opening.Top));
    }

    private bool checkTR(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Top && dir == GameManager.Opening.Right) ||
            (openingDirection == GameManager.Opening.Right && dir == GameManager.Opening.Top));
    }

    private bool checkTop()
    {
        return Physics2D.OverlapCircle(transform.position + (verticalDistance * Vector3.up), 1) != null;
    }

    private bool checkBot()
    {
        return Physics2D.OverlapCircle(transform.position + (-verticalDistance * Vector3.up), 1) != null;
    }

    private bool checkLeft()
    {
        return Physics2D.OverlapCircle(transform.position + (horizontalDistance * Vector3.left), 1) != null;
    }

    private bool checkRight()
    {
        return Physics2D.OverlapCircle(transform.position + (-horizontalDistance * Vector3.left), 1) != null;
    }

    private void updateBotList(List<GameObject> rooms)
    {
        rooms.AddRange(templates.bRooms);

        if (!checkTop())
            rooms.AddRange(templates.tbRooms);

        if (!checkLeft())
            rooms.AddRange(templates.blRooms);

        if (!checkRight())
            rooms.AddRange(templates.brRooms);
    }

    private void updateTopList(List<GameObject> rooms)
    {
        rooms.AddRange(templates.tRooms);

        if (!checkBot())
            rooms.AddRange(templates.tbRooms);

        if (!checkLeft())
            rooms.AddRange(templates.tlRooms);

        if (!checkRight())
            rooms.AddRange(templates.trRooms);
    }

    private void updateLeftList(List<GameObject> rooms)
    {
        rooms.AddRange(templates.lRooms);

        if (!checkBot())
            rooms.AddRange(templates.blRooms);

        if (!checkTop())
            rooms.AddRange(templates.tlRooms);

        if (!checkRight())
            rooms.AddRange(templates.lrRooms);
    }

    private void updateRightList(List<GameObject> rooms)
    {
        rooms.AddRange(templates.rRooms);

        if (!checkBot())
            rooms.AddRange(templates.brRooms);

        if (!checkLeft())
            rooms.AddRange(templates.lrRooms);

        if (!checkTop())
            rooms.AddRange(templates.trRooms);
    }
}
