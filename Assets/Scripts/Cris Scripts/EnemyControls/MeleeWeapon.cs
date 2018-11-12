using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    public int dmg = 1;
    public bool deflection;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && tag == "Enemy Weapon") // Weapon hits player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Bullet" && tag == "Enemy Weapon" && deflection)
        {
            Debug.Log(collision.GetComponent<Rigidbody2D>().rotation);
            collision.transform.Rotate(new Vector3 (0,0,1), collision.GetComponent<Rigidbody2D>().rotation);
        }
    }

}
