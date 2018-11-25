using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModifier : MonoBehaviour {
    public float speedMultiplier = 2;
    public float setSpeedUpTimer = 2f;
    public float distance = 1f;

    private PlayerController player;

    private bool speedUp = false;
    private float speedUpTimer;

	// Use this for initialization
	void Awake () {
        player = GetComponent<PlayerController>();
        speedUp = false;
        speedUpTimer = setSpeedUpTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift) && !speedUp)
            StartSpeedUp();

        FaceMouse();

        if (speedUp)
        {
            speedUpTimer -= Time.deltaTime;

            if (speedUpTimer <= 0)
                EndSpeedUp();
        }
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
    }
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(mousePos.x, mousePos.y);
        }
    }
}
