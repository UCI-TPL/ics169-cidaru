using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float movementSpeed;

    public int dmg = 1;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 move = transform.up * movementSpeed * Time.deltaTime;
        rb2d.MovePosition(rb2d.position + move);
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
