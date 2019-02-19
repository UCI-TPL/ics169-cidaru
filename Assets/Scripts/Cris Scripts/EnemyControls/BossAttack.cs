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

    private Health hp;
    private int phase1Health;
    private int phase2Health;

    private float originalChargeDist;

    private void Start()
    {
        hp = GetComponent<Health>();
        phase1Health = (int) (phase1Percent * hp.startingHealth);
        phase2Health = (int) (phase2Percent * hp.startingHealth);
        originalChargeDist = GetComponent<ChargeMovement>().chargeDistance;
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
        GetComponent<ChargeMovement>().chargeDistance = originalChargeDist * 0.75f;
        Debug.Log(isCharging());
        if (!isCharging())
        {
            spawner.SetActive(true);
            GetComponent<Enemy>().movement.enabled = !spawner.GetComponent<Spawner>().spawning;
        }
        else
        {
            spawner.SetActive(false);
            GetComponent<Enemy>().movement.enabled = true;
        }
    }

    private void Phase2()
    {
        ///TODO: implement lol
        Phase1();
    }
}
