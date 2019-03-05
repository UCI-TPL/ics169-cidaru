using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    // States the destroyable object can be in
    public enum DestroyableRotatingState
    {
        Normal,
        Succing,
        Rotato
    }

    public GameObject destroyedObject;

    // Speed at which the object rotates while in the vortex
    public float rotationSpeed = 200f;

    public bool isVase = false;
    public bool isTrojan = false;

    // Center of vortex in which the object rotates around
    protected Vector3 center;

    // Rigidbody2D of object
    protected Rigidbody2D rb2d;

    // Current state in which the object is in
    protected DestroyableRotatingState currentState = DestroyableRotatingState.Normal;

    // Radius of the object from the center of vortex to it
    protected float radius = 0f;

    // Health of the destroyable object
    private Health hp;

	// Use this for initialization
	void Awake () {
        // Initializes variables
        hp = GetComponent<Health>();

        rb2d = GetComponent<Rigidbody2D>();
        currentState = DestroyableRotatingState.Normal;
        radius = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        // If hp reaches 0, start death
		if (hp.dead())
        {
            Death();
        }

        // During Succing state, properly sets position of the object and rotation
        // During Rotato state, rotates object around center and brings it closer to the center as it rotates
        if (currentState == DestroyableRotatingState.Succing)
        {
            // Removes rotation freezing
            rb2d.freezeRotation = false;

            // Calculates distance of object relative to center of vortex
            Vector2 difference = transform.position - center;

            // Calculates angle using the distance calculated
            float angle = Vector2.Angle(Vector2.right, difference);

            // Based on position, takes negative angle if below the center
            if (transform.position.y - center.y < 0)
                angle = -angle;

            transform.eulerAngles = new Vector3(0, 0, angle);

            // Start rotating after setting values
            currentState = DestroyableRotatingState.Rotato;
        }
        else if (currentState == DestroyableRotatingState.Rotato)
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
        currentState = DestroyableRotatingState.Succing;
        rb2d.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.identity;
    }

    // Function to end the vortex and reset values to indicate it is no longer within the vortex
    public void endVortex()
    {
        // Resets current state, layer, rotation, and constraints
        currentState = DestroyableRotatingState.Normal;

        gameObject.layer = 0;

        transform.rotation = Quaternion.identity;

        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Destroys object when called
    public virtual void Death()
    {
        GameObject rubble = new GameObject();

        if (!isTrojan)
            rubble = Instantiate(destroyedObject, transform.position, Quaternion.identity);

        if (isVase)
            rubble.GetComponent<SpritePicker>().pickVaseRubbleSprite(GetComponent<SpriteRenderer>().sprite);

        if (GetComponent<DropController>())
            GetComponent<DropController>().calculateDrop();

        Destroy(gameObject);
    }
}
