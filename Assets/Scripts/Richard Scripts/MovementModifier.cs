using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementModifier : MonoBehaviour {
    public float speedMultiplier = 2;
    public float setSpeedUpTimer = 2f;
    public float setCooldownTimer = 0.5f;

    public Slider cooldownUI;

    private PlayerController player;

    private bool speedUp = false;
    private bool cooldown = false;
    private float speedUpTimer;
    private float cooldownTimer;

	// Use this for initialization
	void Awake () {
        player = GetComponent<PlayerController>();
        speedUp = false;
        speedUpTimer = setSpeedUpTimer;

        cooldownUI.maxValue = setCooldownTimer;
        cooldownUI.value = setCooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift) && !speedUp && !cooldown)
            StartSpeedUp();

        if (speedUp)
        {
            speedUpTimer -= Time.deltaTime;

            if (speedUpTimer <= 0)
                EndSpeedUp();
        }

        if (cooldown)
            CooldownEffect();
	}

    private void StartSpeedUp()
    {
        speedUp = true;

        player.currentSpeed *= speedMultiplier;

        speedUpTimer = setSpeedUpTimer;
    }

    private void EndSpeedUp()
    {
        speedUp = false;

        player.currentSpeed /= speedMultiplier;

        cooldown = true;
        cooldownTimer = setCooldownTimer;
    }

    private void CooldownEffect()
    {
        cooldownTimer -= Time.deltaTime;

        cooldownUI.value = setCooldownTimer - cooldownTimer;

        if (cooldownTimer <= 0)
        {
            cooldownUI.value = setCooldownTimer;

            cooldown = false;
        }
    }
}
