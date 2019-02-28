using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMovement : EnemyMovement
{
    [Header("Attack-based")]
    public float distanceBeforeAttack; //the distance from the player before it'll stop to attack

    private bool attacking;

    protected override void setStartVars()
    {
        base.setStartVars();
        attacking = false;

    }

    private void FixedUpdate()
    {
        updateAnimations();
        if (!attacking)
            attacking = distanceBeforeAttack >= Vector3.Distance(target.position, transform.position);

        canMove = !attacking;
        canSearch = !attacking;
    }


    public bool getAttacking()
    {
        //true if the enemy is paused to attack
        return attacking;
    }

    public void setAttacking(bool b)
    {
        attacking = b;
    }
}
