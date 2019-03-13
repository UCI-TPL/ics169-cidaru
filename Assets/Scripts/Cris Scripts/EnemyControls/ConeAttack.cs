using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAttack : EnemyAttack
{
    public GameObject weapon;
    public GameObject weaponEcho;
    public float chargeTime = 1f; //the amount of time it takes to charge attack
    public float attackTime = 2f; //the amount of time it attacks for

    private ConeMovement movement; //the movement specifically paired w this script
    private float totalTimer;
    private float chargeTimer;
    private float attackTimer;

    private bool chargedAnim;
    private bool attackAnim;

    public void Start()
    {
        totalTimer = 0f;
        chargeTimer = 0f;
        attackTimer = 0f;
        movement = GetComponent<ConeMovement>();

        chargedAnim = false;
        attackAnim = false;
    }

    public override void Attack()
    {
        if (movement.getAttacking())
        {
            if (chargeTimer < chargeTime)
            {
                //Debug.Log("charging...");
                chargeTimer += Time.deltaTime;
                chargeWeapon();
            }
            else
            {
                //Debug.Log("Attacking");
                attackTimer += Time.deltaTime;
                attackWeapon();
            }
            if (attackTimer >= attackTime)
            {
                //Debug.Log("Reseting");
                resetWeapon();
                chargeTimer = 0;
                attackTimer = 0;
                movement.setAttacking(false);
            }
        }
    }

    private void chargeWeapon()
    {
        //changes rotation of weapon so that it's clear it's charging
        //if (!chargedAnim)
        //    weapon.transform.Rotate(new Vector3(0, 0, 45));
        weapon.transform.Rotate(new Vector3(0, 0, 1));
        chargedAnim = true;
    }

    private void attackWeapon()
    {
        //changes rotation and look of the weapon while it's attacking
        //also the colliders, the colliders change too
        if (!attackAnim)
        {
            SpriteRenderer sr = weapon.GetComponent<SpriteRenderer>();
            for (int i=0; i<16; i++)
            {
                GameObject echo = Instantiate(weaponEcho, transform.position, Quaternion.identity);

                echo.GetComponent<SpriteRenderer>().color = new Color(sr.color.r, sr.color.g,  sr.color.b, .5f);
                echo.transform.Rotate(new Vector3(0, 0, -10 * i * Mathf.Sign(transform.localScale.x)));
                echo.GetComponent<SpriteRenderer>().flipX = Mathf.Sign(transform.localScale.x) == -1;
                Debug.Log(echo.GetComponent<SpriteRenderer>().transform.localScale);
                Destroy(echo, 0.25f);
            }

            weapon.transform.Rotate(new Vector3(0, 0, 175));
        }
        attackAnim = true;
    }

    private void resetWeapon()
    {
        //resets weapon to original position
        weapon.GetComponent<MeleeWeapon>().resetRotations();
        chargedAnim = false;
        attackAnim = false;
    }
}
