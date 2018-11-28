using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    public float minDist;
    public float maxDist;

    private PlayerController player;
    private float movementSpeed;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        movementSpeed = player.currentSpeed;

        if (Vector3.Distance(transform.position, player.transform.position) > minDist)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.transform.position) > maxDist)
            transform.position = player.transform.position;
    }
}
