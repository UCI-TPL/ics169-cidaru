using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    // List of enemy spawners within the room
    public List<GameObject> enemySpawners;

    // List of turrets within the room
    public List<GameObject> turrets;

    // List of door colliders blocking the room
    public List<GameObject> doorColliders;

    public HighlightRoom roomSprite;

    [Header("Portal Room Properties")]
    public bool portalRoom = false;
    public NextLevel portal;

    [Header("Room Trigger Type")]
    public bool singleRoomTrigger = true;
    public bool doubleLRTrigger = false;
    public bool doubleRLTrigger = false;
    public bool doubleTBTrigger = false;
    public bool doubleBTTrigger = false;

    // Check if room is active (enemies spawned and doors closed)
    private bool active;

    // List of enemies after being spawned
    private List<GameObject> enemies = new List<GameObject>();

    // Check if the room has been cleared (enemies killed)
    private bool cleared;

    // Check if the room has been triggered
    private bool triggered;

    // Room sprite to be represented in the map
    private SpriteRenderer sr;

    private RespawnRoom roomFoundChecker;

    public void Awake()
    {
        // Initially set the room to not be active
        active = false;

        // Set all enemy spawners to not be active
        foreach (GameObject enemy in enemySpawners)
            enemy.SetActive(false);

        // Set all turrets to not be active
        foreach (GameObject turret in turrets)
            turret.SetActive(false);

        // Set all door colliders to not be active
        foreach (GameObject dc in doorColliders)
            dc.SetActive(false);

        // Initialize the sprite render of the minimap sprite
        sr = transform.parent.Find("Minimap Sprite").GetComponent<SpriteRenderer>();

        roomFoundChecker = transform.parent.GetComponent<RespawnRoom>();

        if (roomFoundChecker.roomFound)
            sr.enabled = true;
        else
            sr.enabled = false;

        // Set room cleared and trigger to false
        cleared = false;
        triggered = false;
    }

    private void Update()
    {
        // If the room is currently active, check when it is cleared
        if (active)
            checkRoomCleared();

        if (cleared && portalRoom && !portal.isActive())
            portal.Activate();

        // When the room has been triggered and the camera is not panning, start the room
        if (triggered && !GameManager.gm.cameraPanning)
        {
            // Function to start the room
            StartRoom();

            // If the room is not cleared yet spawn everything within room
            if (checkEnemiesAvaliable() && !cleared && !active)
                SpawnRoom();
            else
                cleared = true;

            // Disable trigger
            triggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When player enters into room
        if (collision.tag == "Player")
        {
            // If the room sprite has not been revealed yet, then reveal it on minimap
            if (sr.enabled == false)
            {
                roomFoundChecker.roomFound = true;
                roomFoundChecker.copyRoom.GetComponentInChildren<SpriteRenderer>().enabled = true;
                sr.enabled = true;
            }

            HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

            foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                roomSpriteController.resetRoom();

            roomSprite.highlightRoomSprite();

            // Disables all turrets on the map
            disableTurrets();

            // Updates the current player sprite on the minimap and pans camera
            if (doubleLRTrigger)
                GameManager.gm.updateDoubleLRMinimapPosition(transform.parent.position + new Vector3(11, 0, 0));
            else if (doubleRLTrigger)
                GameManager.gm.updateDoubleLRMinimapPosition(transform.parent.position + new Vector3(-12, 0, 0));
            else if (doubleTBTrigger)
                GameManager.gm.updateDoubleTBMinimapPosition(transform.parent.position + new Vector3(0, -10, 0));
            else if (doubleBTTrigger)
                GameManager.gm.updateDoubleTBMinimapPosition(transform.parent.position + new Vector3(0, 9, 0));
            else
                GameManager.gm.updateMinimapPosition(transform.parent.position);

            // Enables trigger
            triggered = true;
        }
    }

    // Function to start the room when player enters
    public void StartRoom()
    {
        // Spawns all the turrets within the room
        foreach (GameObject turret in turrets)
            turret.SetActive(true);
    }

    // Function to spawn enemies within the room when player enters for first time
    public void SpawnRoom()
    {
        // Sets the room to be active
        active = true;

        // Enables all the enemy spawners within the room
        foreach (GameObject enemy in enemySpawners)
            enemy.SetActive(true);

        // Finds all the enemies spawned
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        // Enables all the doors within the room
        foreach (GameObject dc in doorColliders)
            dc.SetActive(true);
    }

    // Check if the enemy spawners have not spawned yet
    public bool checkEnemiesAvaliable()
    {
        return enemySpawners.Count != 0;
    }

    // Check if the enemies have been cleared
    public void checkRoomCleared()
    {
        // Checks if the enemies spawned have been killed
        foreach (GameObject enemy in enemies)
        {
            // If not cleared, return
            if (enemy != null)
                return;
        }

        // Set active to false and clear to true
        active = false;
        cleared = true;

        if (portalRoom && !portal.isActive())
            portal.Activate();

        // Disable all doors after clearing
        foreach (GameObject dc in doorColliders)
            dc.SetActive(false);

        GetComponent<AudioSource>().Play();
    }

    // Disable all turrets not in the room
    public void disableTurrets()
    {
        // Find all turrets on the map
        List<GameObject> globalTurrets = new List<GameObject>();
        globalTurrets.AddRange(GameObject.FindGameObjectsWithTag("Turret"));

        // Disables all turrets not in the current room
        foreach (GameObject globalTurret in globalTurrets)
        {
            if (!turrets.Contains(globalTurret))
                globalTurret.SetActive(false);
        }
    }
}
