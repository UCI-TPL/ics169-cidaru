using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMovement : EnemyMovement {

    #region Charge Vars
    [Header("Charging")]
    public GameObject movableTarget;
    public float chargeDistance = 0f; //The closest the player needs to be for charge
    public float chargeTime = 0f; //The time takes to charge (how long it waits)
    public float chargePower = 0f; //The speed/power increase multiplier
    public bool weaponFollowsPlayer;

    private bool charged; //Whether or not the enemy has just charged
    private Vector3 chargedTarget;
    #endregion

    protected override void setStartVars()
    {
        base.setStartVars();
        charged = false;
        movableTarget.transform.position = player.transform.position;
        if (target == player.transform)
        {
            target = movableTarget.transform;
        }
    }

    private void FixedUpdate()
    {
        updateAnimations();

        if (charged && !canMove)
        {
            seekTarget();
            chargeAnimations();
            if (weaponFollowsPlayer)
                chargeWeaponAnimations();
            movableTarget.transform.position = player.transform.position;
        }
        if (!charged && !canMove)
        {
            movableTarget.transform.position = player.transform.position;
            if (anim)
                anim.SetBool("chargedRun", false);
            resetRotations(); //Resets (rotation-based) animations
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Enemy")){
            base.MoveAwayFrom(collision.transform.position);
        }
        if (collision.transform.tag.Contains("Player"))
        {
            StartCoroutine(Wait(.3f));
            speed = originalSpeed;
            charged = false;
        }
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void Pursue()
    {
        if (chargeTime != 0)
            ChargeBasedMovement();
        else
        {
            base.Pursue();
        }
    }

    #region Charging
    private void ChargeBasedMovement()
    {
        if (!charged)
        {
            if (distFromPlayer() <= chargeDistance)
            {
                Charge();
            }
            movableTarget.transform.position = player.transform.position;
        }
        else //charged == true
        {
            movableTarget.transform.position = chargedTarget;
            if ((int)distFromTarget(target.position) == 0)
            {
                StartCoroutine(Wait(.5f));
                speed = originalSpeed;
                charged = false;
            }
        }
        MoveTo(target.position);
        updateAnimations();
    }

    private void Charge()
    {
        chargedTarget = seekTarget();
        StartCoroutine(Charging(chargeTime));
        speed += (speed * chargePower);
        MoveTo(target.position);
        charged = true;
    }
    #endregion

    protected override void updateAnimations()
    {
        base.updateAnimations();  
        if (anim)
            anim.SetBool("charging", charged);
    }

    private void chargeAnimations()
    {
        if (tag.Contains("Enemy Boss"))
        {
            //Horse "Animations"
            float newZrotation = startRotation.z;
            if (movableTarget.transform.position.x < transform.position.x)
                newZrotation -= 1;
            else
                newZrotation += 1;
            transform.Rotate(new Vector3(startRotation.x, startRotation.y, newZrotation));
            GetComponent<HorseWheels>().Charge(chargeTime);
        }
        if (anim)
        {
            anim.SetBool("chargedRun", false);
        }
    }

    private void chargeWeaponAnimations()
    {
        MeleeWeapon weapon = GetComponentInChildren<MeleeWeapon>();
        if (!weapon)
            return;
        
        if (weaponFollowsPlayer)
        {
            //Makes the weapon follow the player
            Vector3 newUp = new Vector3(weapon.transform.position.x - player.transform.position.x,
                                weapon.transform.position.y - player.transform.position.y);
            weapon.transform.rotation = Quaternion.LookRotation(Vector3.forward, -newUp);
        }
        else
        {
            ////Turns the weapon parallel to the "ground"
            weapon.transform.Rotate(new Vector3(weapon.transform.rotation.x,
                                                weapon.transform.rotation.y,
                                                -80));
        }
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
        canMove = false;
        chargeWeaponAnimations();
        yield return new WaitForSeconds(secs);
        chargedTarget = seekTarget(); //Resets target to adjust for player's position upon charge start
        if (anim)
            anim.SetBool("chargedRun", true);
        canMove = true;
    }

    private Vector3 seekTarget()
    {
        //// Finds the player and pinpoints the target beyond the player depending on the charge power
        Vector3 dir = Vector3.Normalize((transform.position - player.transform.position));
        Vector3 aquiredTarget = player.transform.position + dir * (-chargePower); //targeting a distance beyond the player
        Debug.DrawLine(transform.position, aquiredTarget, Color.red);
        return aquiredTarget;
    }

    public void cancelCharge()
    {
        /// Resets variables so that when called it sets the enemy
        /// to its original, uncharged state
        charged = false;
        canMove = true;
        movableTarget.transform.position = player.transform.position;
    }
}
