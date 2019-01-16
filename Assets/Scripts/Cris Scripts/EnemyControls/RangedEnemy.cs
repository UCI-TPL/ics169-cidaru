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

    private float fireTimer;

    private void Awake()
    {
        fireTimer = setFireTimer;
    }

    public override void Attack()
    {
        Shoot();
    }

    private void Shoot()
    {
        fireTimer -= Time.deltaTime;
        weapon.GetComponent<Animator>().SetBool("Shooting", true);

        //weapon.transform.rotation = Quaternion.LookRotation(weapon.transform.forward, weapon.transform.position - player.transform.position);

        // Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            weapon.GetComponent<Animator>().SetBool("Shooting", false);
            GameObject newBullet = Instantiate(bullet, weapon.transform.position, Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position));

            newBullet.tag = "Enemy Bullet";

            fireTimer = setFireTimer;
        }
    }
}
