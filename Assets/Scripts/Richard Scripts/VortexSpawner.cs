using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : MonoBehaviour {

    public float movementSpeed;
    public GameObject vortex;

    private Rigidbody2D rb2d;
    private Vector3 location;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        float step = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, location, step);

        if (transform.position == location)
        {
            Instantiate(vortex, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setLocation(Vector3 loc)
    {
        location = new Vector3(loc.x, loc.y, 0);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            Instantiate(vortex, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
