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
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && babyBomb.isAbilityReady())
            {
                babyBomb.PutOnCooldown();

                Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
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
        slowBomb.initAbility();
    }
}
