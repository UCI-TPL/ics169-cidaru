using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
    public GameObject bulletEffect;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && tag == "Enemy Bullet") // Enemy bullet hits player
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if ((col.tag == "Enemy" || col.tag == "Enemy Boss") && tag == "Player Bullet")
        {
            if (col.GetComponent<Health>().getInvincible())
            {
                /// This is the part to take out if we just want the sword to reflect
                /// and not the guys
                reflect();
            }
            else
            {
                col.GetComponent<Health>().TakeDamage(dmg);

                Instantiate(bulletEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        } else if ((col.tag == "Player" || col.tag == "Enemy") && tag == "Vortex Projectile")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (col.tag == "Enemy" && tag == "Rotating Bullet") {
            col.GetComponent<Health>().TakeDamage(dmg);

            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (col.tag == "Obstacle" && tag != "Rotating Bullet")
        {
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (col.tag == "Destroyable")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void reflect()
    {
        transform.up = Vector2.Perpendicular(transform.up);//Vector3.Reflect(transform.up, Vector2.Perpendicular(transform.position));
    }
}
