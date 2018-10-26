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
        }

    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentMovement = BulletMovementState.Succing;
        transform.rotation = Quaternion.identity;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && tag == "Enemy Bullet") // Enemy bullet hits player
        {
            col.gameObject.GetComponent<Health>().TakeDamage(dmg);
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Enemy" && tag == "Player Bullet")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(dmg);

            Destroy(gameObject);
        }
    }
}
