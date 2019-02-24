using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : Projectile
{
    // Effects to be created after bullet is destroyed
    public GameObject bulletCharacterEffect;
    public GameObject bulletObstacleEffect;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && tag == "Enemy Bullet") // Enemy bullet hits player
        {
            // Deals damage to player
            col.GetComponent<Health>().TakeDamage(dmg);

            // Creates effect and deactivates object
            Instantiate(bulletCharacterEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if ((col.tag == "Enemy" || col.tag == "Enemy Boss") && tag == "Player Bullet") // Player bullet hits enemy/boss
        {
            if (col.GetComponent<Health>().getInvincible())
            {
                /// This is the part to take out if we just want the sword to reflect
                /// and not the guys
                reflect();
                GetComponent<AudioSource>().Play();
            }
            else
            {
                // Deals damage to enemy/boss
                col.GetComponent<Health>().TakeDamage(dmg);

                // Creates effect and deactivates object
                Instantiate(bulletCharacterEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
        else if (col.tag == "Enemy" && tag == "Vortex Projectile") // Vortex bullet hits player or enemy
        {
            // Deals damage to player/enemy/boss
            col.GetComponent<Health>().TakeDamage(dmg);

            // Creates effect and deactivates object
            Instantiate(bulletCharacterEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (col.tag == "Enemy" && tag == "Rotating Bullet")
        { // Rotating bullet hits enemy
            // Deals damage to enemy
            col.GetComponent<Health>().TakeDamage(dmg);

            // Creates effect and deactivates object
            Instantiate(bulletCharacterEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (col.tag == "Obstacle" && tag != "Rotating Bullet") // Rotating bullet hits obstacle
        {
            // Creates effect and deactivates object
            Instantiate(bulletObstacleEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (col.tag == "Destroyable") // Any bullet hits a destroyable object
        {
            // Deals damage to obstacle
            col.GetComponent<Health>().TakeDamage(dmg);

            // Creates effect and deactivates object
            Instantiate(bulletObstacleEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (col.tag == "EnemyBulletPassThrough" && tag == "Player Bullet")
        {
            // Creates effect and deactivates object
            Instantiate(bulletObstacleEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void reflect()
    {
        if (tag == "Player Bullet")
        {
            tag = "Enemy Bullet";
            GetComponent<TrailRenderer>().startColor = Color.red;
        }
        //transform.up = Vector2.Perpendicular(transform.up);//Vector3.Reflect(transform.up, Vector2.Perpendicular(transform.position));
        transform.up *= -1;
    }

    public override void OnBecameInvisible()
    {
        print("inside override");
        if (currentMovement == ProjectileMovementState.Normal)
            gameObject.SetActive(false);
    }
}
