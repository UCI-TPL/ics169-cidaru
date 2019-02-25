using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TeleportMovement : EnemyMovement
{
    /*
     * Movement designed for future Ranged enemies
     * Enemies will "teleport," that is they will choose a
     * location to move to, disappear, and reappear at that location
     */

    [Header("Teleporation")]
    public float timeBetweenTeleport = 3f; //the amount of time before teleporting
    public float minDist = 2f; //the min distance to teleport
    public float maxDist = 6f; //the max distance to teleport

    private float timer = 0f;

    protected override void Start()
    {
        base.Start();
        canMove = false;
    }

    public override void Move(bool aggressing)
    {
        ///Enemy teleports, takes some time to shoot, then teleports again
        timer += Time.deltaTime;
        if (timer >= timeBetweenTeleport)
        {
            teleport();
        }
        updateAnimations();
    }

    private void teleport()
    {
        ///Teleports enemy
        canMove = false;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        float dist = Random.Range(minDist,maxDist);
        Vector3 newPos = direction * dist + transform.position;

        NNInfo nn = AstarPath.active.GetNearest(newPos, NNConstraint.Default);
        RaycastHit2D hit = Physics2D.Raycast(nn.clampedPosition, transform.position - player.transform.position);
        if (hit && !hit.transform.CompareTag("Obstacle") &&
            Vector3.Distance(nn.clampedPosition, player.transform.position) >= minDist)
        {
            tr.position = nn.clampedPosition;
            timer = 0;
        }
        //else
        //    Debug.Log("Hit or miss... I guess it missed huh...?");
    }
}
