using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMovement : EnemyMovement {

    #region Charge Vars
    [Header("Charging")]
    public float chargeDistance = 0f; //The closest the player needs to be for charge
    public float chargeTime = 0f; //The time takes to charge (how long it waits)
    public float chargePower = 0f; //The percent speed/power is increased

    private bool charged; //Whether or not the enemy has just charged
    #endregion

    private void FixedUpdate()
    {
        updateAnimations();
        if (charged && !move)
        {
            seekTarget();
            chargeAnimations();
        }
        else if (!charged)
            resetRotations(); //Resets (rotation-based) animations
    }

    protected override void setStartVars()
    {
        base.setStartVars();
        charged = false;
        currentTarget = player.transform.position;
    }

    protected override void Patrol()
    {
        base.Patrol();
        currentTarget = base.currentPatrolPoint.position;
    }

    protected override void Pursue()
    {
        if (chargeTime != 0)
            ChargeBasedMovement();
        else
            base.Pursue();
    }

    #region Charging
    private void ChargeBasedMovement()
    {
        if (distFromPlayer() <= chargeDistance && !charged)
        {
            Charge();
        }
        else
        {
            if ((int)distFromTarget(currentTarget) == 0)
            {
                if (charged)
                {
                    StartCoroutine(Wait(.5f));
                    currentSpeed = originalSpeed;
                    charged = false;
                }
                else
                    currentTarget = player.transform.position;
            }
            MoveTo(currentTarget);
        }
    }

    private void Charge()
    {
        seekTarget();
        StartCoroutine(Charging(chargeTime));
        currentSpeed += (currentSpeed * chargePower);
        MoveTo(currentTarget);
        charged = true;
    }
    #endregion

    protected override void updateAnimations()
    {
        base.updateAnimations();
    }

    private void chargeAnimations()
    {
        if (tag.Contains("Enemy Boss")){
            //Horse "Animations"
            float newZrotation = startRotation.z;
            if (currentTarget.x < transform.position.x)
                newZrotation -= 1;
            else
                newZrotation += 1;
            transform.Rotate(new Vector3(startRotation.x, startRotation.y, newZrotation));
        }
    }

    private void chargeWeaponAnimations()
    {
        MeleeWeapon weapon = GetComponentInChildren<MeleeWeapon>();
        if (!weapon)
            return;

        weapon.transform.Rotate(new Vector3(weapon.transform.rotation.x,
                                            weapon.transform.rotation.y,
                                            -80));
    }

    private void resetRotations()
    {
        transform.rotation = startRotation;
        MeleeWeapon weapon = GetComponentInChildren<MeleeWeapon>();
        if (weapon)
            weapon.resetRotations();
    }

    public IEnumerator Charging(float secs)
    {
        move = false;
        chargeWeaponAnimations();
        yield return new WaitForSeconds(secs);
        seekTarget(); //Resets target to adjust for player's position upon charge start
        move = true;
    }

    private void seekTarget()
    {
        // Finds the player and pinpoints the target beyond the player depending on the charge power
        Vector3 dir = Vector3.Normalize((transform.position - player.transform.position));
        currentTarget = player.transform.position + dir * -(chargePower + 1);
        Debug.DrawLine(transform.position, currentTarget, Color.red);
    }
}
