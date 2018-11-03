using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

    public EnemyAttack attackStyle;
    public EnemyMovement movement;
    public int aggroRange;

    protected GameObject player;
    private Health hp;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = GetComponent<Health>();
	}
	
	void FixedUpdate () {
        if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            attackStyle.Attack();
        }
        checkDeath();
	}

    private void checkDeath()
    {
        // Checks if enemy is dead and destroys them
        if (hp.dead())
        {
            Destroy(gameObject);
        }
    }
}
