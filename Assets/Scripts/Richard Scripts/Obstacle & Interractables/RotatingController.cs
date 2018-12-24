using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingController : MonoBehaviour {

    // States a baby object can be in
    public enum BabyState
    {
        Normal,
        Succing,
        Rotato
    }

    // Speed at which the object rotates while in the vortex
    public float rotationSpeed = 200f;

    // Center of vortex in which the object rotates around
    protected Vector3 center;

    // Rigidbody2D of object
    protected Rigidbody2D rb2d;

    // Current state in which the object is in
    protected BabyState currentState = BabyState.Normal;

    // Radius of the object from the center of vortex to it
    protected float radius = 0f;

    // Use this for initialization
    void Awake()
    {
        // Initializes variables
        rb2d = GetComponent<Rigidbody2D>();
        currentState = BabyState.Normal;
        radius = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        // During Succing state, properly sets position of the object and rotation
        // During Rotato state, rotates object around center and brings it closer to the center as it rotates
        if (currentState == BabyState.Succing)
        {
            // Calculates distance of object relative to center of vortex
            Vector2 difference = transform.position - center;

            // Calculates angle using the distance calculated
            float angle = Vector2.Angle(Vector2.right, difference);

            // Based on position, takes negative angle if below the center
            if (transform.position.y - center.y < 0)
                angle = -angle;

            transform.eulerAngles = new Vector3(0, 0, angle);

            // Start rotating after setting values
            currentState = BabyState.Rotato;
        }
        else if (currentState == BabyState.Rotato)
        {
            // Calculates current distance of the object from the center
            radius = Vector3.Distance(transform.position, center);

            // If radius is greater than value set, bring the object closer
            if (radius >= 0.03f)
                radius -= 0.01f;

            // Orientates object in respect to orbit
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.deltaTime);

            // Moves object along the orbit path
            rb2d.MovePosition(center + radius * new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180)));
        }
    }

    // Function to begin succing and rotating of object
    // param: c, center of the vorext to be passed in
    public void startVortex(Vector3 c)
    {
        // Sets the center, current state, and resets values
        center = c;
        currentState = BabyState.Succing;
        transform.rotation = Quaternion.identity;
    }

    // Function to end the vortex and reset values to indicate it is no longer within the vortex
    public void endVortex()
    {
        // Resets current state and rotation
        currentState = BabyState.Normal;
        
        transform.rotation = Quaternion.identity;
    }
}
