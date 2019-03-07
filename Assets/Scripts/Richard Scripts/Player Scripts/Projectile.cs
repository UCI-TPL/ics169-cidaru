using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // States a projectile object can be in
    public enum ProjectileMovementState
    {
        Normal,
        Succing,
        Rotato
    }

    // Speed at which the projectile object normally moves
    public float movementSpeed;

    // Speed at which the projectile object rotates while in the vortex
    public float rotationSpeed = 200f;

    // Amount of damage projectile will inflict
    public int dmg = 1;

    // Center of vortex in which the projectile object rotates around
    protected Vector3 center;

    // Rigidbody2D of projectile object
    protected Rigidbody2D rb2d;

    // Current state in which the projectile object is in
    protected ProjectileMovementState currentMovement = ProjectileMovementState.Normal;

    // Radius of the projectile object from the center of vortex to it
    protected float radius = 0f;

    public virtual void Awake()
    {
        // Initializes variables
        rb2d = GetComponent<Rigidbody2D>();
        currentMovement = ProjectileMovementState.Normal;
        radius = 0f;
    }

    public virtual void Update()
    {
        #region Old Rotato
        /*
        // Center then Rotato
        if (currentMovement == BulletMovementState.Normal)
        {
            Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        } else if (currentMovement == BulletMovementState.Succing)
        {
            if (transform.position == center)
            {
                currentMovement = BulletMovementState.Rotato;
                return;
            }

            float step = (movementSpeed / 2) * Time.deltaTime;
            rb2d.position = Vector3.MoveTowards(transform.position, center, step);
        } else if (currentMovement == BulletMovementState.Rotato)
        {
            float dist = Mathf.Abs(Vector3.Distance(transform.position, center));
            if (dist <= 0.5f)
            {
                radius += 0.01f;
            }

            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.deltaTime);
            rb2d.MovePosition(center + radius * new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180)));
        }*/
        #endregion Old Rotato

        // Rotato into distance
        // Durring Normal state, moves in direction bullet is pointed at
        // During Succing state, properly sets position of the object and rotation
        // During Rotato state, rotates object around center and brings it closer to the center as it rotates
        if (currentMovement == ProjectileMovementState.Normal)
        {
            // Calculates and moves bullet in direction it is facing
            Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        }
        else if (currentMovement == ProjectileMovementState.Succing)
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
            currentMovement = ProjectileMovementState.Rotato;
        }
        else if (currentMovement == ProjectileMovementState.Rotato)
        {
            // Calculates current distance of the object from the center
            radius = Vector3.Distance(transform.position, center);

            // If radius is greater than value set, bring the object closer
            if (radius >= 0.3f)
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
        currentMovement = ProjectileMovementState.Succing;
        transform.rotation = Quaternion.identity;
    }

    // Function to end the vortex and reset values to indicate it is no longer within the vortex
    public void endVortex()
    {
        // Resets current state
        currentMovement = ProjectileMovementState.Normal;
        if (GetComponent<TrailRenderer>())
            GetComponent<TrailRenderer>().startColor = Color.cyan;
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Called when the object is invisible to the camera
    public virtual void OnBecameInvisible()
    {
        // If object is not withing vortex and is off screen, destroy the object
        if (currentMovement == ProjectileMovementState.Normal)
            Destroy(gameObject);
    }
}
