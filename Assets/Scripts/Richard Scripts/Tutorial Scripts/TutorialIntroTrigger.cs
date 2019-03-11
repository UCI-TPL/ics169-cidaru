using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIntroTrigger : MonoBehaviour
{
    public HighlightRoom roomSprite;

    public GameManager.TutorialStates stateToChange;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.gm.currentState != GameManager.TutorialStates.ShootRoom)
            {
                GameManager.gm.nextState = stateToChange;
            }

            GameManager.gm.checkIntroDoorOpen();

            HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

            foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                roomSpriteController.resetRoom();

            roomSprite.highlightRoomSprite();

            GameManager.gm.updateMinimapPosition(transform.parent.position);

        }
    }
}
