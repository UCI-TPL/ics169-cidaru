using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    public Ability babyBomb;
    public Ability hasteBomb;
    public Ability slowBomb;

    //public int manaCost = 20;

    //private Mana manaController;

    private void Awake()
    {
        //manaController = GetComponent<Mana>();

        initAbilityCharges();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && babyBomb.isAbilityReady())
            {
                babyBomb.PutOnCooldown();

                Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
            } else if (Input.GetKeyDown(KeyCode.Alpha2) && hasteBomb.isAbilityReady())
            {
                hasteBomb.PutOnCooldown();

                Instantiate(hasteBomb.abilityPrefab, transform.position, Quaternion.identity);
            } else if (Input.GetKeyDown(KeyCode.Alpha3) && slowBomb.isAbilityReady())
            {
                slowBomb.PutOnCooldown();

                Instantiate(slowBomb.abilityPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void initAbilityCharges()
    {
        babyBomb.initAbility();
        hasteBomb.initAbility();
        slowBomb.initAbility();
    }
}
