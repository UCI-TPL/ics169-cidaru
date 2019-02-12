using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class HorseWheels : MonoBehaviour {
    public int rotationSpeed = 100;
    public int chargeRotSpeed = 500;

    public GameObject leftWheel;
    public GameObject rightWheel;
    private Vector3 prevPos;
    private float currVel;

    bool charging = false;

    private void Start()
    {
        prevPos = transform.position;

    }

    void FixedUpdate()
    {
        currVel = (prevPos - transform.position).magnitude / Time.deltaTime;
        if (!charging)
        {
            float rotSpeed = rotationSpeed * currVel * Time.deltaTime;
            wheelSpin(rotSpeed);
        }
        prevPos = transform.position;

    }

    void wheelSpin(float rotSpeed)
    {
        leftWheel.transform.Rotate(new Vector3(0, 0, -1 * rotSpeed));
        rightWheel.transform.Rotate(new Vector3(0, 0, -1 * rotSpeed));
    }

    public void Charge(float chargeDuration)
    {
        wheelSpin(chargeRotSpeed);
        StartCoroutine(ChargeTimer(chargeDuration));
    }

    IEnumerator ChargeTimer(float chargeDuration)
    {
        charging = true;
        yield return new WaitForSeconds(chargeDuration);
        charging = false;
    }
}
