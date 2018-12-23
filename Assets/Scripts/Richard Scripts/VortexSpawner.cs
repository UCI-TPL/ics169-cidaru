using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : MonoBehaviour {

    public float movementSpeed;
    public GameObject vortex;

    private Vector3 location;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        Move();

        if (Input.GetButtonDown("Right Bumper") || Input.GetMouseButtonDown(1))
        {
            Instantiate(vortex, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setLocation(Vector3 loc)
    {
        location = new Vector3(loc.x, loc.y, 0);
    }

    /*
    private void MouseMovement()
    {
        float step = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, location, step);

        if (transform.position == location)
        {
            Instantiate(vortex, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }*/

    private void Move()
    {
        Vector2 move = transform.up * movementSpeed * Time.deltaTime;
        rb2d.MovePosition(rb2d.position + move);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // FIX THIS...DETERMINE WHAT WILL POP A VORTEX
        if (col.tag == "Obstacle" || col.tag == "Enemy" || col.tag == "Destroyable" || col.tag == "Vortex" || col.tag == "Turret")
        {
            Instantiate(vortex, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
