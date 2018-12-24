using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    // Values for how close or far the pet can be from the player
    public float minDist;
    public float maxDist;

    // Object to follow
    private PlayerController player;

    // Movement speed of pet
    private float movementSpeed;

	// Use this for initialization
	void Start () {
        // Finds player to follow
        player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Sets movement speed to be the same as the players (Boosts if player also boosts)
        movementSpeed = player.currentSpeed;

        // Move towards the player if distance is not close enough
        if (Vector3.Distance(transform.position, player.transform.position) > minDist)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        // Teleports to the player if past the max distance from player
        if (Vector3.Distance(transform.position, player.transform.position) > maxDist)
            transform.position = player.transform.position;
    }
}
