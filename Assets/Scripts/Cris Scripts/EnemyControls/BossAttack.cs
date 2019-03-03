using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack {
    /* Defines how the Boss attacks
     * Trojan Horse: Spawns out of itself incrementally,
     *               damages player when runs them over.
     */

    [Header("Base Info")]
    public int dmg = 3;
    public float phase1Percent = (float) 0.75; //the percent of health before the boss enters phase1
    public float phase2Percent = (float) 0.25; //the percent of health before the boss enters phase2
    
    /// HP stuff
    private Health hp;
    private int phase1Health;
    private int phase2Health;

    /// Extra Charge stuff
    private float originalChargeDist;
    private ChargeMovement chargeMove;

    [Header("Phase 1")]
    /// Spawning stuff
    public GameObject spawner;
    public float spawnRate; //the rate at which it spawns spawners
    public float spawnDuration; //the amount of time it takes to spawn

    private float spawnerTimer;
    private float spawnDurationTimer;

    //[Header("Phase 2")]
    //public int numCharges;


    private void Start()
    {
        hp = GetComponent<Health>();
        phase1Health = (int) (phase1Percent * hp.startingHealth);
        phase2Health = (int) (phase2Percent * hp.startingHealth);

        spawnerTimer = spawnRate;
        spawnDurationTimer = 0;
    }

    private void Awake()
    {
        originalChargeDist = GetComponent<ChargeMovement>().chargeDistance;
        chargeMove = (ChargeMovement)GetComponent<Enemy>().movement;
    }

    public override void Attack()
    {
        if (hp.currentHealth >= phase2Health && hp.currentHealth <= (phase1Health))
        {
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

    private void Phase1()
    {
        spawnerTimer += Time.deltaTime;

        if (spawnerTimer >= spawnRate)
        {
            spawnDurationTimer += Time.deltaTime;
            GetComponent<ChargeMovement>().cancelCharge();
            GetComponent<ChargeMovement>().enabled = false;

            if (spawnDurationTimer >= spawnDuration)
            {
                Instantiate(spawner, transform.position, transform.rotation);
                spawnDurationTimer = 0f;
                spawnerTimer = 0f;
                GetComponent<ChargeMovement>().enabled = true;
            }
        }
    }

    private void Phase2()
    {
        ///TODO: implement lol
    }
}
