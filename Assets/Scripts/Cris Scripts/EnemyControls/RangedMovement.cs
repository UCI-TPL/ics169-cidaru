using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMovement : EnemyMovement {
    /*
     * Movement designed for the Ranged Enemies
     */
    
    [Header("Ranged Movement")]
    public float minDistToPlayer; // This determines how close the enemy can get to the player before it stops
    public GameObject movableTarget;

    protected override void setStartVars()
    {
        base.setStartVars();
        movableTarget.transform.position = player.transform.position;
        if (target == player.transform)
        {
            target = movableTarget.transform;
        }
    }

    protected override void Pursue()
    {
        updateTarget();
        MoveTo(movableTarget.transform.position);
    }

    protected override void updateAnimations()
    {
        ///Updates visuals and animations
        if (anim)
            anim.SetBool("walking", canMove);

        if (player.transform.position.x < transform.position.x)
            this.transform.localScale = new Vector3(-1.0f * startScale.x, transform.localScale.y);
        else
            this.transform.localScale = new Vector3(1.0f * startScale.x, transform.localScale.y);
    }

    private void updateTarget()
    {
        Vector3 newPos = (transform.position - player.transform.position).normalized * minDistToPlayer + player.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.position + player.transform.position);
        if (hit && (hit.transform.CompareTag("Obstacle") || hit.transform.CompareTag("Tree") || hit.transform.CompareTag("Destroyable")))
            movableTarget.transform.position = player.transform.position;
        else
            movableTarget.transform.position = newPos;
    }
}