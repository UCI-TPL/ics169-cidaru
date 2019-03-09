using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMomHitControl : MonoBehaviour
{
    public float movementSpeed = 0.001f;

    public Animator trumpAnimator;

    private Vector3 targetPosition = new Vector3(4, -0.2f, 0);

    // Update is called once per frame
    void Update()
    {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.eulerAngles.z > -89 || transform.eulerAngles.z < -91)
            transform.Rotate(0, 0, -120 * Time.deltaTime);

        if (transform.position.x - targetPosition.x > -0.001f)
        {
            trumpAnimator.SetBool("move", true);
            enabled = false;
        }
    }
}
