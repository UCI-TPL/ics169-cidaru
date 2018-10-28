using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour {
    /* Defines the attack behavior for Melee Enemies */

    public GameObject weapon;

    private GameObject player;
    private Health hp;

	private void Awake() {
        hp = GetComponent<Health>();
	}

    private void Start()
    {
        player = GameObject.Find("Player");
    }
	
	private void Update () {
        Attack();
	}

    private void Attack()
    {
        //TODO: Define attack for melee enemies
    }
}
