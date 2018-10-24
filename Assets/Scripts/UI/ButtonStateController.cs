using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStateController : MonoBehaviour {

    public List<Button> buttons;
    public List<EventTrigger> eventTriggers;

    public void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        foreach (EventTrigger eventTrigger in eventTriggers)
        {
            eventTrigger.enabled = true;
        }
    }

    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        foreach (EventTrigger eventTrigger in eventTriggers)
        {
            eventTrigger.enabled = false;
        }
    }
}
