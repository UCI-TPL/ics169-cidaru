using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

    // Room templates that store the information involving the type of rooms necessary
    private RoomTemplates templates;

	// Use this for initialization
	void Start () {
        // Finds the room template
        templates = GameManager.gm.GetComponent<RoomTemplates>();

        // Add the object this is attached to into the room template "rooms" array
        templates.rooms.Add(gameObject);
	}
}
