using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack {
    /* Defines how the Boss attacks
     * Trojan Horse: Spawns out of itself incrementally,
     *               damages player when runs them over.
     */

    public int dmg = 3;
    public float phase1Percent = (float) 0.75; //the percent of health before the boss enters phase1
    public float phase2Percent = (float) 0.25; //the percent of health before the boss enters phase2

    public GameObject spawner;
    public int numChargesBeforeBirth;

    private Health hp;
    private int phase1Health;
    private int phase2Health;

    private float originalChargeDist;
    private ChargeMovement chargeMove;

    private void Start()
    {
        hp = GetComponent<Health>();
        phase1Health = (int) (phase1Percent * hp.startingHealth);
        phase2Health = (int) (phase2Percent * hp.startingHealth);
    }

    private void Awake()
    {
        originalChargeDist = GetComponent<ChargeMovement>().chargeDistance;
        chargeMove = (ChargeMovement)GetComponent<Enemy>().movement;
        spawner.GetComponent<Spawner>().setContinuousSpawn(false);
    }

    public override void Attack()
    {
        if (hp.currentHealth >= phase2Health && hp.currentHealth <= (phase1Health))
        {
            Debug.Log("Phase 1");
            Phase1();
        }
        else if (hp.currentHealth <= (phase2Health))
            Phase2();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") // Hit player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }
    }

    private bool isCharging()
    {
        return GetComponent<EnemyMovement>().originalSpeed < GetComponent<EnemyMovement>().speed;
    }

    private void Phase1()
    {
        //GetComponent<ChargeMovement>().chargeDistance = originalChargeDist * 0.75f;
        spawner.GetComponent<Spawner>().setContinuousSpawn(false);

        if (chargeMove.getNumTimesCharged() >= numChargesBeforeBirth+1)
        {
            GetComponent<Enemy>().movement.enabled = false;
            spawner.SetActive(true);
            spawner.GetComponent<Spawner>().Spawn();
            chargeMove.resetNumTimesCharged();
        }
        else if (chargeMove.getNumTimesCharged() == 0 && !spawner.GetComponent<Spawner>().spawning)
        {
            GetComponent<Enemy>().movement.enabled = true;
        }
    }

    private void Phase2()
    {
        ///TODO: implement lol
        Phase1();
    }
}
