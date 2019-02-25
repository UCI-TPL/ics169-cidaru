using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    public GameManager.Opening openingDirection;

    private RoomTemplates templates;
    private GameObject grid;
    private int rand;
    private bool spawned;

    private int verticalDistance = 19;
    private int horizontalDistance = 23;

    private void Awake()
    {
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();
        grid = GameObject.Find("Grid");

        spawned = false;

        Invoke("Spawn", 1f);
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

            botRooms.Clear();
        }
        else if (openingDirection == GameManager.Opening.Top)
        {
            List<GameObject> topRooms = new List<GameObject>();
            updateTopList(topRooms);

            rand = Random.Range(0, topRooms.Count);

            GameObject room = Instantiate(topRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;

            topRooms.Clear();
        }
        else if (openingDirection == GameManager.Opening.Left)
        {
            List<GameObject> leftRooms = new List<GameObject>();
            updateLeftList(leftRooms);

            rand = Random.Range(0, leftRooms.Count);

            GameObject room = Instantiate(leftRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;

            leftRooms.Clear();
        }
        else if (openingDirection == GameManager.Opening.Right)
        {
            List<GameObject> rightRooms = new List<GameObject>();
            updateRightList(rightRooms);

            rand = Random.Range(0, rightRooms.Count);

            GameObject room = Instantiate(rightRooms[rand], transform.position, Quaternion.identity);
            room.transform.parent = grid.transform;

            rightRooms.Clear();
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

        if (isDestroyerThere())
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
                // CHANGE FOR THE FUTRUE
                
                GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawn Point");

                List<RoomSpawner> collidingSpawner = new List<RoomSpawner>();

                foreach (GameObject spawner in spawners)
                {
                    if (spawner.transform.position == transform.position)
                        collidingSpawner.Add(spawner.GetComponent<RoomSpawner>());

                }

                CancelInvoke();

                otherRS.spawned = true;
                otherRS.CancelInvoke();

                if (spawned)
                    return;

                if (checkBL(otherRS.openingDirection))
                {
                    // Bottom and Left Room Connected
                    List<GameObject> blRooms = new List<GameObject>();
                    updateBLRooms(blRooms);

                    rand = Random.Range(0, blRooms.Count);

                    GameObject room = Instantiate(blRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
                else if (checkBR(otherRS.openingDirection))
                {
                    // Bottom and Right Room Connected
                    List<GameObject> brRooms = new List<GameObject>();
                    updateBRRooms(brRooms);

                    rand = Random.Range(0, brRooms.Count);

                    GameObject room = Instantiate(brRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
                else if (checkTL(otherRS.openingDirection))
                {
                    // Top and Left Room Connected
                    List<GameObject> tlRooms = new List<GameObject>();
                    updateTLRooms(tlRooms);

                    rand = Random.Range(0, tlRooms.Count);

                    GameObject room = Instantiate(tlRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
                else if (checkTR(otherRS.openingDirection))
                {
                    // Top and Right Room Connected
                    List<GameObject> trRooms = new List<GameObject>();
                    updateTRRooms(trRooms);

                    rand = Random.Range(0, trRooms.Count);

                    GameObject room = Instantiate(trRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
                else if (checkTB(otherRS.openingDirection))
                {
                    // Top and Bottom Room Connected
                    List<GameObject> tbRooms = new List<GameObject>();
                    updateTBRooms(tbRooms);

                    rand = Random.Range(0, tbRooms.Count);

                    GameObject room = Instantiate(tbRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
                else if (checkLR(otherRS.openingDirection))
                {
                    // Left and Right Room Connected
                    List<GameObject> lrRooms = new List<GameObject>();
                    updateLRRooms(lrRooms);

                    rand = Random.Range(0, lrRooms.Count);

                    GameObject room = Instantiate(lrRooms[rand], transform.position, Quaternion.identity);
                    room.transform.parent = grid.transform;
                }
            }

            spawned = true;
        }
    }

    // Checks which two directions are being connected for two rooms
    #region Two Direction Check
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

    private bool checkTB(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Top && dir == GameManager.Opening.Bottom) ||
            (openingDirection == GameManager.Opening.Bottom && dir == GameManager.Opening.Top));
    }

    private bool checkLR(GameManager.Opening dir)
    {
        return ((openingDirection == GameManager.Opening.Left && dir == GameManager.Opening.Right) ||
            (openingDirection == GameManager.Opening.Right && dir == GameManager.Opening.Left));
    }
    #endregion Two Direction Check

    // Checks if the area is clear in a single direction (up, down, left, right)
    #region Single Direction Check
    private bool isDestroyerThere()
    {
        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position)
                return true;
        }

        return false;
    }

    private bool isTopClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (verticalDistance * Vector3.up), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (verticalDistance * Vector3.up))
                return false;
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
                return false;
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
                return false;
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
                return false;
        }

        return true;
    }
    #endregion Single Direction Check

    // Checks if the area is clear double of a single direction
    #region Double Single Direction Check

    private bool isDoubleLeftClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (horizontalDistance * Vector3.left * 2))
                return false;
        }

        foreach (GameObject sp in spawnPoints)
        {
            if (sp.transform.position == transform.position + (horizontalDistance * Vector3.left * 2) + (verticalDistance * Vector3.up))
                return false;

            if (sp.transform.position == transform.position + (horizontalDistance * Vector3.left * 2) + (-verticalDistance * Vector3.up))
                return false;

            if (sp.transform.position == transform.position + (horizontalDistance * Vector3.left * 3))
                return false;
        }

        return true;
    }

    private bool isDoubleRightClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (-horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (-horizontalDistance * Vector3.left * 2))
                return false;
        }

        foreach (GameObject sp in spawnPoints)
        {
            if (sp.transform.position == transform.position + (-horizontalDistance * Vector3.left * 2) + (verticalDistance * Vector3.up))
                return false;

            if (sp.transform.position == transform.position + (-horizontalDistance * Vector3.left * 2) + (-verticalDistance * Vector3.up))
                return false;

            if (sp.transform.position == transform.position + (-horizontalDistance * Vector3.left * 3))
                return false;
        }

        return true;
    }

    private bool isDoubleTopClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (-horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (verticalDistance * Vector3.up * 2))
                return false;
        }

        foreach (GameObject sp in spawnPoints)
        {
            if (sp.transform.position == transform.position + (verticalDistance * Vector3.up * 2) + (horizontalDistance * Vector3.left))
                return false;

            if (sp.transform.position == transform.position + (verticalDistance * Vector3.up * 2) + (-horizontalDistance * Vector3.left))
                return false;

            if (sp.transform.position == transform.position + (verticalDistance * Vector3.up * 3))
                return false;
        }

        return true;
    }

    private bool isDoubleBotClear()
    {
        //return Physics2D.OverlapCircle(transform.position + (-horizontalDistance * Vector3.left), 1) == null;

        GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");

        foreach (GameObject d in destroyers)
        {
            if (d.transform.position == transform.position + (-verticalDistance * Vector3.up * 2))
                return false;
        }

        foreach (GameObject sp in spawnPoints)
        {
            if (sp.transform.position == transform.position + (-verticalDistance * Vector3.up * 2) + (horizontalDistance * Vector3.left))
                return false;

            if (sp.transform.position == transform.position + (-verticalDistance * Vector3.up * 2) + (-horizontalDistance * Vector3.left))
                return false;

            if (sp.transform.position == transform.position + (-verticalDistance * Vector3.up * 3))
                return false;
        }

        return true;
    }
    #endregion Double Single Direction Check

    // Updates the possible rooms that are necessary that can be produced from a single opening
    #region Update Single Direction Room Listing
    private void updateBotList(List<GameObject> rooms)
    {
        if (isTopClear())
            rooms.AddRange(templates.tbRooms);

        if (isLeftClear())
            rooms.AddRange(templates.blRooms);

        if (isRightClear())
            rooms.AddRange(templates.brRooms);

        if (isTopClear() && isDoubleTopClear())
            rooms.AddRange(templates.doubleBTRooms);

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

        if (isBotClear() && isDoubleBotClear())
            rooms.AddRange(templates.doubleTBRooms);

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

        if (isRightClear() && isDoubleRightClear())
            rooms.AddRange(templates.doubleLRRooms);

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

        if (isLeftClear() && isDoubleLeftClear())
            rooms.AddRange(templates.doubleRLRooms);

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
    #endregion Update Single Direction Room Listing

    // Updates the possible rooms that are necessary that can be produced from a double opening
    #region Update Double Direction Room Listing
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
        if (isRightClear())
            rooms.AddRange(templates.tlrRooms);

        if (isBotClear())
            rooms.AddRange(templates.tlbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.tlRooms);
        }
    }

    private void updateTRRooms(List<GameObject> rooms)
    {
        if (isLeftClear())
            rooms.AddRange(templates.tlrRooms);

        if (isBotClear())
            rooms.AddRange(templates.trbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.trRooms);
        }
    }

    private void updateTBRooms(List<GameObject> rooms)
    {
        if (isLeftClear())
            rooms.AddRange(templates.tlbRooms);

        if (isRightClear())
            rooms.AddRange(templates.trbRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.tbRooms);
        }
    }

    private void updateLRRooms(List<GameObject> rooms)
    {
        if (isTopClear())
            rooms.AddRange(templates.tlrRooms);
        
        if (isBotClear())   
            rooms.AddRange(templates.blrRooms);

        if (templates.checkMinRooms() || rooms.Count == 0)
        {
            if (templates.checkMaxRooms())
                rooms.Clear();

            rooms.AddRange(templates.lrRooms);
        }
    }
    #endregion Update Double Direction Room Listing
}
