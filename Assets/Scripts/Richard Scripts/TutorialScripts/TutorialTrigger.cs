using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject[] doors;

    public HighlightRoom roomSprite;

    public bool portalTrigger = false;

    void Awake()
    {
        foreach (GameObject door in doors)
            door.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.gm.updateMinimapPosition(transform.parent.position);

            gameObject.SetActive(false);

            HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

            foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                roomSpriteController.resetRoom();

            roomSprite.highlightRoomSprite();

            if (portalTrigger)
                GameObject.Find("Portal").GetComponent<NextLevel>().Activate();

            foreach (GameObject door in doors)
            {
                if (gameObject.name == "Entry Trigger")
                    door.SetActive(true);
                else
                    door.SetActive(false);
            }
        }
    }
}
