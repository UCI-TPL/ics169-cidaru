using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour {
    public GameObject selectedObject;

    [Header("Title Screen Alternative")]
    public bool isTitleScreen = false;
    public GameObject alternateSelect;
    public Button continueButton;

    private GameObject originalSelect;

    private EventSystem eventSystem;
    private bool buttonSelected;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        originalSelect = selectedObject;

        if (isTitleScreen)
        {
            if (!continueButton.IsInteractable())
                selectedObject = alternateSelect;
            else
                selectedObject = originalSelect;
        }
    }
    
    private void Update() {
		if ((Input.GetAxisRaw("Vertical") != 0) && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    public void ResetButtonSelected()
    {
        buttonSelected = false;
    }
}
