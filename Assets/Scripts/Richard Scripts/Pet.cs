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

    // For animations and stuff
    private Animator anim;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        // Finds player to follow
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        // Sets movement speed to be the same as the players (Boosts if player also boosts)
        movementSpeed = player.currentSpeed;

        // Move towards the player if distance is not close enough
        if (Vector3.Distance(transform.position, player.transform.position) > minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

            if (transform.position.x < player.transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        // Teleports to the player if past the max distance from player
        if (Vector3.Distance(transform.position, player.transform.position) > maxDist)
            transform.position = player.transform.position;

        if (!anim.GetBool("walking"))
        {
            anim.SetFloat("timeIdle", Time.deltaTime + anim.GetFloat("timeIdle"));
        }
        else
        {
            anim.SetFloat("timeIdle", 0f);
        }
    }
}
