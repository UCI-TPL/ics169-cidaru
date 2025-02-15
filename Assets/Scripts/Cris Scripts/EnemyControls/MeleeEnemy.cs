﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAttack {
    /* Defines the ATTACK behavior for Melee Enemies 
     * Standard Melee Enemies attack using their weapons, 
     * so this just serves for clarity in how they attack.
     * However, it also allows for the "whirlwind" ability.
     */
    
    public GameObject weapon;
    public float whirlwindLimit;
    public float restLimit;
    public bool tasmanian; //spin like the tasmanian devil/diablo beserkers
    [HideInInspector]
    public bool spinning; //whether or not the enemy is currently spinning

    private int whirlwindSpeed = 40;
    private float whirlwindTimer;
    private float restTimer;
    private bool deflect;

    private Health hp;
    private Quaternion startRotation;

    public void Start()
    {
        hp = GetComponent<Health>();
        startRotation = transform.rotation;
        whirlwindTimer = 0;
        restTimer = 0;
        if (weapon){
            deflect = weapon.GetComponent<MeleeWeapon>().deflection;
        }
    }

    public override void Attack()
    {
        if (GetComponent<Health>().dead() && weapon)
            Destroy(weapon);

        if (whirlwindLimit == 0) //No whirlwind, no worries
            return;

        doWhirlWind();
    }

    private void doWhirlWind()
    {
        /* The whirlwind spins the melee weapon for (whirlwindSpeed) seconds
         * and makes the attacker invincible for that duration.
         * After which, it takes a brief rest, where it is vulnerable.
         * It also makes the weapon deflect bullets while spinning.
         */

        if (whirlwindTimer >= whirlwindLimit)
        {
            spinning = false;
            makeVulnerable();
            restTimer += Time.deltaTime;
        }
        else
        {
            spinning = true;
            animate();
            hp.setInvincible(true);
            weapon.GetComponent<MeleeWeapon>().deflection = deflect;
            whirlwindTimer += Time.deltaTime;
        }
        if (restTimer >= restLimit)
        {
            whirlwindTimer = 0;
            restTimer = 0;
        }
    }

    private void animate()
    {
        if (tasmanian)
            tasSpin();
        else
            colorGuardTwirl();
    }

    private void colorGuardTwirl()
    {
        //Appearance/Animation for the type of spin
        weapon.transform.Rotate(new Vector3(0, 0, -whirlwindSpeed));
    }

    private void tasSpin()
    {
        //Appearance/Animation for the type of spin
        this.transform.Rotate(new Vector3(0, whirlwindSpeed, 0));
    }

    private void swishySwishy()
    {
        //Appearance/Animation for the type of swish
        this.transform.Rotate(new Vector3(0, 0, whirlwindSpeed));
    }

    private void resetRotations()
    {
        //Reset weapon's rotations based on the enemy's sprite direction
        if (weapon)
            weapon.GetComponent<MeleeWeapon>().resetRotations();

        //Reset enemy's rotations (mainly for tasSpin)
        transform.rotation = startRotation;
    }

    public void makeVulnerable()
    {
        resetRotations();
        hp.setInvincible(false);
        if (weapon)
            weapon.GetComponent<MeleeWeapon>().deflection = false;
        return;
    }
}
