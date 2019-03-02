using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShootAttack : RangedEnemy
{
    public int numShots = 6; //number of shots fired at once
    public int restTime = 3; //amount of time between bursts
    public bool stopMoveOnRest; //whether or not the enemy stops moving when resting

    private int shotsFired;
    private float restTimer;

    private void Awake()
    {
        fireTimer = setFireTimer;
        shotsFired = 0;
        restTimer = 0;
    }

    public override void Attack()
    {
        animateWeapon();
        burstShoot();
    }

    protected void burstShoot()
    {
        if (shotsFired != numShots)
        {
            fireTimer -= Time.deltaTime;
            // Timer to fire bullets on set intervals
            if (fireTimer <= 0f)
            {
                Shoot();
                shotsFired++;
            }
        }
        else
        {
            if (stopMoveOnRest)
                GetComponent<EnemyMovement>().enabled = true;
            restTimer += Time.deltaTime;
            if (restTimer >= restTime)
            {
                shotsFired = 0;
                restTimer = 0;
                GetComponent<EnemyMovement>().enabled = false;
            }
        }
    }
}
