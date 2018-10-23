using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulation : MonoBehaviour{
    public float timeSpeed = 2f;
    public float timeLimit = 2f;
    public GameObject speedForce;
    [HideInInspector]
    public bool powersActive = false;

    private PlayerController pController;
    private float timer = 0f;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!powersActive && Input.GetKey(KeyCode.Space))
        {
            activatePowers();
        }
        if (powersActive)
            checkTimeLimit();

    }

    private void activatePowers()
    {
        adjustSpeed(timeSpeed);
        speedForce.SetActive(true);
        powersActive = true;
    }

    private void deactivatePowers()
    {
        adjustSpeed(1 / timeSpeed);
        speedForce.SetActive(false);
        powersActive = false;
    }


    private void adjustSpeed(float speedMultiplier)
    {
        pController.currentSpeed = pController.currentSpeed * speedMultiplier;
    }

    private void checkTimeLimit()
    {
        if (timer <= timeLimit)
        {
            timer += Time.deltaTime;
        }
        else
        {
            deactivatePowers();
            timer = 0f;
        }
    }

}
