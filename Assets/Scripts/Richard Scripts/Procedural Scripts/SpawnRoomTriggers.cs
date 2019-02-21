using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomTriggers : MonoBehaviour {
    public HighlightRoom roomSprite;

    private void Start()
    {
        roomSprite.highlightRoomSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

            foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                roomSpriteController.resetRoom();

            roomSprite.highlightRoomSprite();

            disableTurret();
            GameManager.gm.updateMinimapPosition(transform.parent.position);
        }
    }

    public void disableTurret()
    {
        List<GameObject> globalTurrets = new List<GameObject>();
        globalTurrets.AddRange(GameObject.FindGameObjectsWithTag("Turret"));


        foreach (GameObject globalTurret in globalTurrets)
        {
            globalTurret.SetActive(false);
        }
    }
}
