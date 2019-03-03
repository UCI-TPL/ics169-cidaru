using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject[] doors;

    public HighlightRoom roomSprite;

    public bool portalTrigger = false;

    [Header("Room Trigger Type")]
    public bool singleRoomTrigger = true;
    public bool doubleLRTrigger = false;
    public bool doubleRLTrigger = false;
    public bool doubleTBTrigger = false;
    public bool doubleBTTrigger = false;

    void Awake()
    {
        foreach (GameObject door in doors)
            door.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (singleRoomTrigger)
                GameManager.gm.updateMinimapPosition(transform.parent.position);
            else if (doubleLRTrigger)
                GameManager.gm.updateDoubleLRMinimapPosition(transform.parent.position + new Vector3(11, 0, 0));
            else if (doubleRLTrigger)
                GameManager.gm.updateDoubleLRMinimapPosition(transform.parent.position + new Vector3(-12, 0, 0));
            else if (doubleTBTrigger)
                GameManager.gm.updateDoubleTBMinimapPosition(transform.parent.position + new Vector3(0, -10, 0));
            else if (doubleBTTrigger)
                GameManager.gm.updateDoubleTBMinimapPosition(transform.parent.position + new Vector3(0, 9, 0));

            gameObject.SetActive(false);

            if (gameObject.name == "Entry Trigger")
            {
                HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

                foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                    roomSpriteController.resetRoom();

                roomSprite.highlightRoomSprite();
            }
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
