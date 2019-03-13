using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float moveDistance;
    public float jumpHeight;
    public float jumpSpeed;
    public float moveSpeed;

    private Vector3 minPosition;
    private Vector3 maxPosition;

    private bool moveRight;
    private bool jumping;
    private bool falling;

    // Start is called before the first frame update
    void Awake()
    {
        minPosition = transform.position - new Vector3(moveDistance, 0, 0);
        maxPosition = transform.position + new Vector3(moveDistance, 0, 0);

        moveRight = true;
        jumping = false;
        falling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.Translate(new Vector3(moveSpeed * Time.unscaledDeltaTime, 0, 0));

            if (transform.position.x > maxPosition.x)
                moveRight = false;
        }
        else
        {
            transform.Translate(new Vector3(-moveSpeed * Time.unscaledDeltaTime, 0, 0));

            if (transform.position.x < minPosition.x)
                moveRight = true;
        }

        if (!jumping)
        {
            int random = Random.Range(1, 11);

            if (random <= 3)
                jumping = true;
        }
        else if (jumping && !falling)
        {
            transform.Translate(new Vector3(jumpSpeed * Time.deltaTime, 0, 0));

            if (transform.position.y > jumpHeight)
                falling = true;
        }

        if (falling)
        {
            transform.Translate(new Vector3(-jumpSpeed * Time.deltaTime, 0, 0));

            if (transform.position.y < -0.4)
            {
                falling = false;
                jumping = false;
            }
        }


    }
}
