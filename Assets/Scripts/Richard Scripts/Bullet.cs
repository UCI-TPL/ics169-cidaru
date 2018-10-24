using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float movementSpeed;

    public int dmg = 1;

    private Rigidbody rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = transform.up * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider col)
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
