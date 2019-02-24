using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMovement : EnemyMovement {
    /*
     * Movement designed for the Ranged Enemies
     */
    
    [Header("Ranged Movement")]
    public float minDistToPlayer; // This determines how close the enemy can get to the player before it stops
    public float margin = 0.1f; // Defines the range away from the min that's still allowed
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

    public override void Move(bool aggressing)
    {
        //if (distFromPlayer() > minDistToPlayer + margin)
        //{
        //    movableTarget.transform.position = (transform.position - player.transform.position).normalized * minDistToPlayer + player.transform.position;
        //    target = movableTarget.transform;
        //}
        //else if (distFromPlayer() < minDistToPlayer - margin){}     
        movableTarget.transform.position = (transform.position - player.transform.position).normalized * minDistToPlayer + player.transform.position;
    }
}
