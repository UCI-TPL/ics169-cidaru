using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyAttack {
    /* Defines the ATTACK behavior for Ranged Enemies 
     Adapted from Richard's EnemyController script 
     */

    public float setFireTimer;
    public GameObject bullet;
    public GameObject weapon;
    //public Animator weaponAnim;

    protected float fireTimer;

    private void Awake()
    {
        fireTimer = setFireTimer;
    }

    public override void Attack()
    {
        animateWeapon();

        fireTimer -= Time.deltaTime;
        // Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            Shoot();
        }
    }

    protected void Shoot()
    {
        weapon.GetComponent<Animator>().SetBool("Shooting", false); // Bow release animation
        GameObject newBullet = Instantiate(bullet, weapon.transform.position, Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position));

        newBullet.tag = "Enemy Bullet";

        fireTimer = setFireTimer;
    }

    protected void animateWeapon()
    {
        // Bow loading animation
        weapon.GetComponent<Animator>().SetBool("Shooting", true);

        // Rotating the Bow to point at Player
        Vector3 newUp = new Vector3(weapon.transform.position.x - player.transform.position.x,
                                    weapon.transform.position.y - player.transform.position.y);
        weapon.transform.rotation = Quaternion.LookRotation(Vector3.forward, newUp);
        if (weapon.name.Equals("Bow"))
            weapon.transform.Rotate(new Vector3(0, 0, 90));
        else if (weapon.name.Equals("Crossbow"))
            weapon.transform.Rotate(new Vector3(0, 0, -90));
        else if (weapon.name.Equals("Gun"))
            weapon.transform.Rotate(new Vector3(0, 0, -90));

        if (GetComponent<Health>().dead() && weapon)
            Destroy(weapon);
    }
}
