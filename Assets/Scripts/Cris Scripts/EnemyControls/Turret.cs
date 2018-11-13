using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyAttack {
    /* Class Summary:
     * Fires whever the gun points are pointing. 
     * Does not target anything, just fires.
     * Made so that it doesn't need health or an aggro range, 
     * but can be used with one (via Enemy component).
     */

    public float setFireTimer;
    public int rotationSpeed;
    public GameObject bullet;
    public GameObject[] gunPoints;

    private float fireTimer;

    private void Awake()
    {
        fireTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (!GetComponent<Enemy>())
            Attack();
    }

    public override void Attack()
    {
        transform.Rotate(Vector3.forward, rotationSpeed);
        Shoot();
    }

    private void Shoot()
    {
        fireTimer -= Time.deltaTime;

        // Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            foreach (GameObject gunPoint in gunPoints)
            {
                GameObject newBullet = Instantiate(bullet, gunPoint.transform.position, Quaternion.LookRotation(Vector3.forward, gunPoint.transform.up));

                newBullet.tag = "Enemy Bullet";
            }

            fireTimer = setFireTimer;
        }
    }
}
