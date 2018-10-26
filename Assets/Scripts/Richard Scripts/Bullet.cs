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

    public int dmg = 1;
    private Vector3 center;

    private Rigidbody2D rb2d;
    private BulletMovementState currentMovement = BulletMovementState.Normal;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        currentMovement = BulletMovementState.Normal;
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

            float step = movementSpeed * Time.deltaTime;
            rb2d.position = Vector3.MoveTowards(transform.position, center, step);
        }
        
    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentMovement = BulletMovementState.Succing;
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
