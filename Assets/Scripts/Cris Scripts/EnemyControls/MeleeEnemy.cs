using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAttack {
    /* Defines the attack behavior for Melee Enemies 
     * Standard Melee Enemies attack using their weapons, 
     * so this just serves for clarity in how they attack.
     * However, it also allows for the "whirlwind" ability.
     */
    
    public GameObject weapon;
    public float whirlwindLimit;

    private int whirlwindSpeed = 40;
    private float whirlwindTimer;
    private float restTimer;

    private Quaternion defaultWeaponRotation;
    private Health hp;

    public void Start()
    {
        hp = GetComponent<Health>();
        whirlwindTimer = 0;
        restTimer = 0;
        defaultWeaponRotation = weapon.transform.rotation;
    }

    public override void Attack()
    {
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
            makeVulnerable();
            restTimer += Time.deltaTime;
        }
        else
        {
            weapon.transform.Rotate(new Vector3(0, 0, whirlwindSpeed));
            hp.setInvincible(true);
            weapon.GetComponent<MeleeWeapon>().deflection = true;
            whirlwindTimer += Time.deltaTime;
        }
        if (restTimer >= whirlwindLimit)
        {
            whirlwindTimer = 0;
            restTimer = 0;
        }
    }

    private void makeVulnerable()
    {
        weapon.transform.rotation = defaultWeaponRotation;
        hp.setInvincible(false);
        weapon.GetComponent<MeleeWeapon>().deflection = false;
        return;
    }
}
