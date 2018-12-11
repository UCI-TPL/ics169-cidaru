using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class HorseWheels : MonoBehaviour {
    public int rotationSpeed = 10;
    public GameObject leftWheel;
    public GameObject rightWheel;
    private Vector3 prevPos;
    private float currVel;

    private void Start()
    {
        prevPos = transform.position;

    }

    void FixedUpdate()
    {
        currVel = (prevPos - transform.position).magnitude / Time.deltaTime;
        float rotSpeed = rotationSpeed  * currVel * Time.deltaTime;
        leftWheel.transform.Rotate(new Vector3(0, 0, -1 * rotSpeed));
        rightWheel.transform.Rotate(new Vector3(0, 0, -1 * rotSpeed));
        prevPos = transform.position;
    }
}
