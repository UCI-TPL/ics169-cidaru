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
            List<GameObject> tempRooms = new List<GameObject>(templates.bottomRooms);
            updateBotList(tempRooms);

            rand = Random.Range(0, tempRooms.Count - 1);

            GameObject room = Instantiate(tempRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Top)
        {
            List<GameObject> tempRooms = new List<GameObject>(templates.topRooms);
            updateTopList(tempRooms);

            rand = Random.Range(0, tempRooms.Count - 1);

            GameObject room = Instantiate(tempRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Left)
        {
            List<GameObject> tempRooms = new List<GameObject>(templates.leftRooms);
            updateLeftList(tempRooms);

            rand = Random.Range(0, tempRooms.Count - 1);

            GameObject room = Instantiate(tempRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Right)
        {
            List<GameObject> tempRooms = new List<GameObject>(templates.rightRooms);
            updateRightList(tempRooms);

            rand = Random.Range(0, tempRooms.Count - 1);

            GameObject room = Instantiate(tempRooms[rand], transform.position, Quaternion.identity);
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
                    GameObject room = Instantiate(templates.blFiller, transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkBR(otherRS.openingDirection))
                {
                    GameObject room = Instantiate(templates.brFiller, transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTL(otherRS.openingDirection))
                {
                    GameObject room = Instantiate(templates.tlFiller, transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTR(otherRS.openingDirection))
                {
                    GameObject room = Instantiate(templates.trFiller, transform.position, Quaternion.identity);
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
        return Physics2D.OverlapCircle(transform.position + (5 * Vector3.up), 1) != null;
    }

    private bool checkBot()
    {
        return Physics2D.OverlapCircle(transform.position + (-5 * Vector3.up), 1) != null;
    }

    private bool checkLeft()
    {
        return Physics2D.OverlapCircle(transform.position + (5 * Vector3.left), 1) != null;
    }

    private bool checkRight()
    {
        return Physics2D.OverlapCircle(transform.position + (-5 * Vector3.left), 1) != null;
    }

    private void updateBotList(List<GameObject> rooms)
    {
        if (checkTop())
            rooms.RemoveAt(1);

        if (checkLeft())
            rooms.RemoveAt(3);

        if (checkRight())
            rooms.RemoveAt(2);
    }

    private void updateTopList(List<GameObject> rooms)
    {
        if (checkBot())
            rooms.RemoveAt(1);

        if (checkLeft())
            rooms.RemoveAt(2);

        if (checkRight())
            rooms.RemoveAt(3);
    }

    private void updateLeftList(List<GameObject> rooms)
    {
        if (checkBot())
            rooms.RemoveAt(0);

        if (checkTop())
            rooms.RemoveAt(3);

        if (checkRight())
            rooms.RemoveAt(2);
    }

    private void updateRightList(List<GameObject> rooms)
    {
        if (checkBot())
            rooms.RemoveAt(0);

        if (checkLeft())
            rooms.RemoveAt(1);

        if (checkTop())
            rooms.RemoveAt(3);
    }
}
