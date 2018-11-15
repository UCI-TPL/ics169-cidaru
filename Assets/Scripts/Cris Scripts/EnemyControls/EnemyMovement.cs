﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    #region Basic Movement Vars
    [Header("Basic Movement")]
    public float originalSpeed = 0f;
    //[HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public bool move;

    private GameObject player;
    private float lastSpeed;
    #endregion

    #region Patrol Vars
    [Header("Patrol")]
    public Transform[] patrolPoints;

    private int currentPatrolIndex;
    private Transform currentPatrolPoint;
    #endregion

    #region Charge Vars
    [Header("Charging")]
    public float chargeDistance = 0f; //the closest the player needs to be for charge
    public float chargeTime = 0f; //the time takes to charge (how long it waits)
    public float chargePower = 0f; //the percent speed/power is increased

    private bool charged;
    private Vector3 currentTarget;
    #endregion

    private Animator anim;
    private Vector3 startScale;

    private void Start()
    {
        anim = GetComponent<Animator>();
        startScale = transform.localScale;
        currentSpeed = originalSpeed;
        lastSpeed = currentSpeed;

        move = true;
        player = GameObject.Find("Player");

        charged = false;
        currentTarget = player.transform.position;

        if (patrolPoints.Length != 0)
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    void FixedUpdate()
    {
        updateAnimations();
        if (!move)
            return;

        //Debug.Log(GetComponent<Enemy>().aggressing);
        if (GetComponent<Enemy>().aggressing)
            Pursue();
        else if (patrolPoints.Length != 0)
            Patrol();
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % (patrolPoints.Length);
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }
        Move(currentPatrolPoint.position);
        currentTarget = currentPatrolPoint.position;
    }

    private void Pursue()
    {
        //Debug.Log("Pursing");
        if (chargeTime != 0)
            ChargeBasedMovement();
        else
        {
            Move(player.transform.position);
            currentTarget = player.transform.position;
        }
    }

    #region Charging
    private void ChargeBasedMovement()
    {
        if (playerCloserThan(chargeDistance) && !charged)
        {
            Charge(currentTarget);
        }
        else
        {
            if ((int) distFromTarget(currentTarget) == 0)
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
            Move(currentTarget);
        }

        //Debug.Log("Charged up: " + charged);
    }

    private void Charge(Vector3 position)
    {
        //Debug.Log("Charging");
        lastSpeed = currentSpeed;
        currentTarget = player.transform.position;
        StartCoroutine(Wait(chargeTime));
        currentSpeed += (currentSpeed * chargePower);
        Move(currentTarget);
        charged = true;
    }
    #endregion

    #region Basic Movement
    private void Move(Vector3 position)
    {
        /* Basic movementthe base bones of how the AI moves */
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
    }

    public IEnumerator Wait(float secs)
    {
        move = false;
        //Debug.Log("Waiting...");
        yield return new WaitForSeconds(secs);
        //Debug.Log("DONE!");
        move = true;
    }
    #endregion

    #region Distance-based Helper Functions
    private bool playerCloserThan(float limit)
    {
        return distFromPlayer() <= limit;
    }

    private bool playerFartherThan(float limit)
    {
        return distFromPlayer() >= limit;
    }

    private float distFromPlayer()
    {
        return distFromTarget(player.transform.position);
    }

    private float distFromTarget(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }
    #endregion

    private void updateAnimations()
    {
        if (anim)
            anim.SetBool("walking", GetComponent<Enemy>().aggressing);

        if (currentTarget.x < transform.position.x)
            this.transform.localScale = new Vector3(-1.0f * startScale.x, transform.localScale.y);
        else
            this.transform.localScale = new Vector3(1.0f * startScale.x, transform.localScale.y);
    }
}
