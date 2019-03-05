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
            GameManager.gm.updateMinimapPosition(transform.parent.position);

            if (GameManager.gm.currentState != GameManager.TutorialStates.ShootRoom)
                GameManager.gm.nextState = stateToChange;

            if (gameObject.name == "Entry Trigger")
            {
                HighlightRoom[] roomSpriteControllers = FindObjectsOfType<HighlightRoom>();

                foreach (HighlightRoom roomSpriteController in roomSpriteControllers)
                    roomSpriteController.resetRoom();

                roomSprite.highlightRoomSprite();
            }
        }
    }
}
