using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    // Bomb abilities
    public Ability babyBomb;
    public Ability slowBomb;

    // Initialize all the values of the ability
    private void Awake()
    {
        initAbilityCharges();
    }

    private void Update()
    {
        // If not in pause state, allow player to use ability
        if (Time.timeScale != 0 && !GameManager.gm.cameraPanning && !GameManager.gm.spawningRooms)
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Left Bumper") || Input.GetButtonDown("Y Button")) && babyBomb.isAbilityReady())
            {
                // Creates baby bomb if avaliable and button is pressed
                babyBomb.PutOnCooldown();

                Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
            } else if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("B Button")) && slowBomb.isAbilityReady())
            {
                // Creates slow bomb if avaliable and button is pressed
                slowBomb.PutOnCooldown();

                Instantiate(slowBomb.abilityPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Initialize all the values of the ability
    private void initAbilityCharges()
    {
        babyBomb.initAbility();
        slowBomb.initAbility();
    }
}
