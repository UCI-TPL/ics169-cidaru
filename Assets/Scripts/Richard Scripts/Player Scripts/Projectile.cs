using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public enum ProjectileMovementState
    {
        Normal,
        Succing,
        Rotato
    }

    public float movementSpeed;
    public float rotationSpeed = 200f;

    public int dmg = 1;

    protected Vector3 center;

    protected Rigidbody2D rb2d;
    protected ProjectileMovementState currentMovement = ProjectileMovementState.Normal;
    protected float radius = 0f;

    public virtual void Awake()
    {
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
        if (currentMovement == ProjectileMovementState.Normal)
        {
            Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        }
        else if (currentMovement == ProjectileMovementState.Succing)
        {
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentMovement = ProjectileMovementState.Rotato;
        }
        else if (currentMovement == ProjectileMovementState.Rotato)
        {
            radius = Vector3.Distance(transform.position, center);
            if (radius >= 0.3f)
            {
                radius -= 0.01f;
            }

            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.deltaTime);

            rb2d.MovePosition(center + radius * new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180)));
        }
    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentMovement = ProjectileMovementState.Succing;
        transform.rotation = Quaternion.identity;
    }

    public void endVortex()
    {
        currentMovement = ProjectileMovementState.Normal;
    }

    public void OnBecameInvisible()
    {
        if (currentMovement == ProjectileMovementState.Normal)
        {
            Destroy(gameObject);
        }
    }
}
