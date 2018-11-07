using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
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
        } else if ((col.tag == "Player" || col.tag == "Enemy") && tag == "Vortex Projectile")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            Destroy(gameObject);
        } else if (col.tag == "Obstacle" && tag != "Rotating Bullet")
        {
            Destroy(gameObject);
        }
    }
}
