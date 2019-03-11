using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementModifier : MonoBehaviour {

    // Speed multiplier to increase the players movement
    public float speedMultiplier = 2;

    // Speed multiplier duration
    public float setSpeedUpTimer = 2f;

    // Cooldown of speed multiplier
    public float setCooldownTimer = 0.5f;

    // Cooldown UI of speed buff
    [HideInInspector]
    public Slider cooldownUI;

    // Player movement controller to modify
    private PlayerController player;

    // Conditions for speed up and cooldown
    private bool speedUp = false;
    private bool cooldown = false;

    // Timers for speed up and cooldown
    private float speedUpTimer;
    private float cooldownTimer;

	// Use this for initialization
	void Awake () {
        //Initializes and finds values
        player = GetComponent<PlayerController>();
        speedUp = false;
        speedUpTimer = setSpeedUpTimer;

        cooldownUI = GameObject.Find("Sprint CD").GetComponent<Slider>();

        cooldownUI.maxValue = setCooldownTimer;
        cooldownUI.value = setCooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {

        // Increase speed if ability is ready and button is pressed
        /*
        if (GameManager.gm.isTutorial)
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("Left Trigger") > 0) && !speedUp && !cooldown && CheckSlowTutorialCondition())
                StartSpeedUp();
        }
        else
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("Left Trigger") > 0) && !speedUp && !cooldown)
                StartSpeedUp();
        }
        */

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("Left Trigger") > 0) && !speedUp && !cooldown)
            StartSpeedUp();


        // During speed up
        if (speedUp)
        {
            // Countdown speed up timer, decrease if active
            speedUpTimer -= Time.deltaTime;

            // Stops speed up when timer is up
            if (speedUpTimer <= 0)
                EndSpeedUp();
        }

        // During cooldown, update cooldown UI
        if (cooldown)
            CooldownEffect();
	}

    private bool CheckSlowTutorialCondition()
    {
        return (GameManager.gm.currentState == GameManager.TutorialStates.SlowRoomStart || GameManager.gm.currentState == GameManager.TutorialStates.SlowRoom ||
            GameManager.gm.currentState == GameManager.TutorialStates.SlowRoomPost || GameManager.gm.currentState == GameManager.TutorialStates.PortalRoomPost);
    }

    // Function to begin movement modifier
    private void StartSpeedUp()
    {
        speedUp = true;

        // Increase speed with multiplier
        player.currentSpeed *= speedMultiplier;

        // Sets speed up timer
        speedUpTimer = setSpeedUpTimer;
    }

    // Function to end movement modifier
    private void EndSpeedUp()
    {
        speedUp = false;

        // Decreases speed to original
        player.currentSpeed /= speedMultiplier;

        // Start cooldown of movement
        cooldown = true;
        cooldownTimer = setCooldownTimer;
    }

    // Cooldown process
    private void CooldownEffect()
    {
        // Countdowns cooldown timer
        cooldownTimer -= Time.deltaTime;

        // Updates cooldown UI
        cooldownUI.value = setCooldownTimer - cooldownTimer;

        // Ends cooldown when countdown is complete
        if (cooldownTimer <= 0)
        {
            cooldownUI.value = setCooldownTimer;

            cooldown = false;
        }
    }

    // Check if player is using the movement modifier
    public bool isSprinting()
    {
        return speedUp;
    }
}
