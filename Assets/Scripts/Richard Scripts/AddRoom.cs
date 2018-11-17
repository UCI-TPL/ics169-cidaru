using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {
    private RoomTemplates templates;

	// Use this for initialization
	void Start () {
        templates = GameObject.Find("GameManager").GetComponent<RoomTemplates>();

        templates.rooms.Add(gameObject);
	}
}
