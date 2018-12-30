using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : MonoBehaviour {
    // Movement speed of the spawner projectile
    public float movementSpeed;

    // Vortex to be spawned by the projectile
    public GameObject vortex;

    // Rigidbody2D of the object
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        // Initializes the rigidbody
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        // Function call to move the projectile spawner
        Move();

        // When Right Bumper or Mouse Right Click is pressed again, stop the projectile movement and spawn vortex at position
        if (Input.GetButtonDown("Right Bumper") || Input.GetMouseButtonDown(1))
        {
            // Spawns the vortex at the location of the projectile
            Instantiate(vortex, transform.position, Quaternion.identity);

            // Destroys the spawner
            Destroy(gameObject);
        }
    }

    #region Old Code
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
    #endregion Old Code

    // Moves the projectile in the direction it is facing
    private void Move()
    {
        // Calculates the movement
        Vector2 move = transform.up * movementSpeed * Time.deltaTime;

        // Moves the object to the position using the calculated movement
        rb2d.MovePosition(rb2d.position + move);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // Pops the vortex when it collides with the appropriate object
        if (col.tag == "Obstacle" || col.tag == "Enemy" || col.tag == "Destroyable" || col.tag == "Vortex" || col.tag == "Turret")
        {
            // Spawns the vortex in the spawner location
            Instantiate(vortex, transform.position, Quaternion.identity);

            // Destroys the vortex spawner
            Destroy(gameObject);
        }
    }
}
