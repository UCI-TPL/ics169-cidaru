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
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();
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

            rand = Random.Range(0, botRooms.Count);

            GameObject room = Instantiate(botRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Top)
        {
            List<GameObject> topRooms = new List<GameObject>();
            updateTopList(topRooms);

            rand = Random.Range(0, topRooms.Count);

            GameObject room = Instantiate(topRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Left)
        {
            List<GameObject> leftRooms = new List<GameObject>();
            updateLeftList(leftRooms);

            rand = Random.Range(0, leftRooms.Count);

            GameObject room = Instantiate(leftRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }
        else if (openingDirection == GameManager.Opening.Right)
        {
            List<GameObject> rightRooms = new List<GameObject>();
            updateRightList(rightRooms);

            rand = Random.Range(0, rightRooms.Count);

            GameObject room = Instantiate(rightRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;
        }

        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            spawned = true;
            return;
        }

        if (other.CompareTag("Spawn Point"))
        {
            RoomSpawner otherRS = other.GetComponent<RoomSpawner>();

            if (!otherRS.spawned && !spawned)
            {
                if (checkBL(otherRS.openingDirection))
                {
                    if (templates.checkMinRooms())
                    {
                        rand = Random.Range(0, templates.blRooms.Length);

                        GameObject room = Instantiate(templates.blRooms[rand], transform.position, Quaternion.identity);
                        room.transform.parent = grid.transform;
                        Destroy(gameObject);
                    } else
                    {
                        rand = Random.Range(0, templates.blrRooms.Length);

                        GameObject room = Instantiate(templates.blRooms[rand], transform.position, Quaternion.identity);
                        room.transform.parent = grid.transform;
                        Destroy(gameObject);
                    }
                    
                }
                else if (checkBR(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.brRooms.Length);

                    GameObject room = Instantiate(templates.brRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTL(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.tlRooms.Length);

                    GameObject room = Instantiate(templates.tlRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                    Destroy(gameObject);
                }
                else if (checkTR(otherRS.openingDirection))
                {
                    rand = Random.Range(0, templates.trRooms.Length);

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

    private bool isTopClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (verticalDistance * Vector3.up), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (verticalDistance * Vector3.up))
            {
                return false;
            }
        }

        return true;
    }

    private bool isBotClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (-verticalDistance * Vector3.up), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (-verticalDistance * Vector3.up))
            {
                return false;
            }
        }

        return true;
    }

    private bool isLeftClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (horizontalDistance * Vector3.left))
            {
                return false;
            }
        }

        return true;
    }

    private bool isRightClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (-horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (-horizontalDistance * Vector3.left))
            {
                return false;
            }
        }

        return true;
    }

    private void updateBotList(List<GameObject> rooms)
    {
        if (isTopClear())
            rooms.AddRange(templates.tbRooms);

        if (isLeftClear())
            rooms.AddRange(templates.blRooms);

        if (isRightClear())
            rooms.AddRange(templates.brRooms);

        if (isTopClear() && isLeftClear())
            rooms.AddRange(templates.tlbRooms);

        if (isTopClear() && isRightClear())
            rooms.AddRange(templates.trbRooms);

        if (isLeftClear() && isRightClear())
            rooms.AddRange(templates.blrRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.bRooms);
        }
    }

    private void updateTopList(List<GameObject> rooms)
    {
        if (isBotClear())
            rooms.AddRange(templates.tbRooms);

        if (isLeftClear())
            rooms.AddRange(templates.tlRooms);

        if (isRightClear())
            rooms.AddRange(templates.trRooms);

        if (isBotClear() && isLeftClear())
            rooms.AddRange(templates.tlbRooms);

        if (isBotClear() && isRightClear())
            rooms.AddRange(templates.trbRooms);

        if (isLeftClear() && isRightClear())
            rooms.AddRange(templates.tlrRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.tRooms);
        }
    }

    private void updateLeftList(List<GameObject> rooms)
    {
        if (isBotClear())
            rooms.AddRange(templates.blRooms);

        if (isTopClear())
            rooms.AddRange(templates.tlRooms);

        if (isRightClear())
            rooms.AddRange(templates.lrRooms);

        if (isTopClear() && isRightClear())
            rooms.AddRange(templates.tlrRooms);

        if (isBotClear() && isRightClear())
            rooms.AddRange(templates.blrRooms);

        if (isTopClear() && isBotClear())
            rooms.AddRange(templates.tlbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.lRooms);
        }
    }

    private void updateRightList(List<GameObject> rooms)
    {
        if (isBotClear())
            rooms.AddRange(templates.brRooms);

        if (isLeftClear())
            rooms.AddRange(templates.lrRooms);

        if (isTopClear())
            rooms.AddRange(templates.trRooms);

        if (isTopClear() && isLeftClear())
            rooms.AddRange(templates.tlrRooms);

        if (isBotClear() && isLeftClear())
            rooms.AddRange(templates.blrRooms);

        if (isTopClear() && isBotClear())
            rooms.AddRange(templates.trbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.rRooms);
        }
    }

    private void updateBLRooms(List<GameObject> rooms)
    {
        if (isRightClear())
            rooms.AddRange(templates.blrRooms);

        if (isTopClear())
            rooms.AddRange(templates.tlbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.blRooms);
        }
    }

    private void updateBRRooms(List<GameObject> rooms)
    {
        if (isLeftClear())
            rooms.AddRange(templates.blrRooms);

        if (isTopClear())
            rooms.AddRange(templates.trbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.brRooms);
        }
    }

    private void updateTLRooms(List<GameObject> rooms)
    {

    }
}
