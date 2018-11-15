using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack {
    /* Defines how the Boss attacks
     * Trojan Horse: Spawns out of itself incrementally,
     *               damages player when runs them over.
     */

    public int dmg = 3;
    public GameObject spawner;
    public float percentHealth = (float) 0.25; //the percents of health dropped before the boss will activate the spawner

    private Health hp;
    private int spawnBasedHealth;

    private void Start()
    {
        hp = GetComponent<Health>();
        spawnBasedHealth = hp.startingHealth;
    }

    public override void Attack()
    {
        Debug.Log(spawnBasedHealth - (percentHealth * spawnBasedHealth));
        if (hp.currentHealth <= (spawnBasedHealth - (percentHealth * spawnBasedHealth)))
        {
            spawner.SetActive(true);
            spawner.GetComponent<Spawner>().reset();
            spawnBasedHealth = spawnBasedHealth - (int) (percentHealth * spawnBasedHealth);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") // Hit player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }
    }
}
