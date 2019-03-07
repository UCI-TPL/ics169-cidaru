using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVortexMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    public GameObject introVortex;

    public IntroEchoController echo;
    public IntroMomControls mom;

    private Vector3 targetPosition;

    public void Awake()
    {
        targetPosition = transform.position - new Vector3(2.9f, 0, 0);
    }

    public void Update()
    {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position.x - targetPosition.x < 0.001f)
        {
            Instantiate(introVortex, transform.position, Quaternion.identity);

            echo.enabled = true;
            mom.enabled = true;

            Destroy(gameObject);
        }
    }
}
