using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyAttack {
    /* Defines the ATTACK behavior for Ranged Enemies 
     Adapted from Richard's EnemyController script 
     */

    public float setFireTimer;
    public GameObject bullet;

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

        // Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position));

            newBullet.tag = "Enemy Bullet";

            fireTimer = setFireTimer;
        }
    }
}
