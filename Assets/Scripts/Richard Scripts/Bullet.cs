using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum BulletMovementState
    {
        Normal,
        Succing,
        Rotato
    }

    public float movementSpeed;
    public float rotationSpeed = 200f;

    public int dmg = 1;
    private Vector3 center;

    private Rigidbody2D rb2d;
    private BulletMovementState currentMovement = BulletMovementState.Normal;
    private float radius = 0f;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        currentMovement = BulletMovementState.Normal;
        radius = 0f;
    }
	
	// Update is called once per frame
	void Update () {
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

        // Rotato into distance
        if (currentMovement == BulletMovementState.Normal)
        {
            Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        }
        else if (currentMovement == BulletMovementState.Succing)
        {
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            print(angle);

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentMovement = BulletMovementState.Rotato;
        }
        else if (currentMovement == BulletMovementState.Rotato)
        {
            radius = Vector3.Distance(transform.position, center);
            if (radius >= 0.5f)
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
        currentMovement = BulletMovementState.Succing;
        transform.rotation = Quaternion.identity;
    }

    public void endVortex()
    {
        currentMovement = BulletMovementState.Normal;
    }

    public void OnBecameInvisible()
    {
        if (currentMovement == BulletMovementState.Normal)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && tag == "Enemy Bullet") // Enemy bullet hits player
        {
            col.GetComponent<Health>().TakeDamage(dmg);
            Destroy(gameObject);
        } else if (col.tag == "Enemy" && tag == "Player Bullet")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            Destroy(gameObject);
        }
    }
}
