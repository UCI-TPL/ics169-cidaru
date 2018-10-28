using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class RangedEnemy : MonoBehaviour {
    /* Defines the attack behavior for Ranged Enemies 
     Adapted from Richard's EnemyController script 
     */

    public float setFireTimer;
    public GameObject bullet;
    public int aggroRange;

    private float fireTimer;
    private GameObject player;
    private Health hp;

    private void Awake()
    {
        fireTimer = 0f;
        hp = GetComponent<Health>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            Shoot();
        checkDeath();
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

    private void checkDeath()
    {
        // Checks if enemy is dead and destorys them
        if (hp.dead())
        {
            Destroy(gameObject);
        }
    }
}
