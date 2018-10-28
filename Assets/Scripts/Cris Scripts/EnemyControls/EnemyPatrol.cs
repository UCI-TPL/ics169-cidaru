using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyMovement {

    public Transform[] patrolPoints;
    public int eyesightMax; //how far the player needs to be for enemy to stop pursuing

    private int currentPatrolIndex;
    private Transform currentPatrolPoint;
    private bool playerSpotted;

    private void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        player = GameObject.Find("Player");
        playerSpotted = false;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            currentPatrolIndex = (currentPatrolIndex+1) % (patrolPoints.Length);
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        playerSpotted = eyesightMax > Vector3.Distance(transform.position, player.transform.position);

        if (playerSpotted)
            Move(player.transform);
        else
            Move(currentPatrolPoint);
    }
}
