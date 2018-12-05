using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    public Ability babyBomb;
    public Ability slowBomb;

    private void Awake()
    {
        initAbilityCharges();
    }

    private void Update()
    {
        if (Time.timeScale != 0 && !GameManager.gm.cameraPanning)
        {
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Left Bumper") || Input.GetButtonDown("Y Button")) && babyBomb.isAbilityReady())
            {
                babyBomb.PutOnCooldown();

                Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
            } else if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("B Button")) && slowBomb.isAbilityReady())
            {
                slowBomb.PutOnCooldown();

                Instantiate(slowBomb.abilityPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void initAbilityCharges()
    {
        babyBomb.initAbility();
        slowBomb.initAbility();
    }
}
