using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulation : MonoBehaviour{
    public float timeSpeed = 1f;
    private PlayerController pController;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            adjustSpeed();
    }

    private void adjustSpeed()
    {
        pController.currentSpeed = pController.originalSpeed * timeSpeed;
    }

}
