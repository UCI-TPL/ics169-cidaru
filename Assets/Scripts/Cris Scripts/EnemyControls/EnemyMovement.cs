using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    #region Basic Movement Vars
    [Header("Basic Movement")]
    public float originalSpeed = 0f;
    public float currentSpeed;
    [HideInInspector]
    public bool move; //Whether or not the enemy should be moving

    protected GameObject player;
    protected Vector3 currentTarget; //The position of the target the enemy is moving towards. Usually player but not always
    #endregion

    #region Patrol Vars
    [Header("Patrol")]
    public Transform[] patrolPoints; //The points the enemy will patrol between until player is in sight

    protected int currentPatrolIndex; //The index of the currentPatrolPoint
    protected Transform currentPatrolPoint; //The current patrol point the enemy is moving towards
    #endregion

    #region Visual Stuff
    protected Animator anim;
    protected Vector3 startScale;
    protected Quaternion startRotation;
    #endregion

    private void Start()
    {
        setStartVars();
    }

    protected virtual void setStartVars()
    {
        ///Starting Visuals
        anim = GetComponent<Animator>();
        startScale = transform.localScale;
        startRotation = transform.rotation;

        ///Starting Speeds
        currentSpeed = originalSpeed;

        ///Starting Misc Variables
        move = true;
        player = GameObject.Find("Player");

        if (patrolPoints.Length != 0)
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    public virtual void Move(bool aggressing)
    {
        /// How the enemy specifically moves, considering everything
        /// Called in the Enemy script
        if (!move)
            return;

        if (aggressing)
            Pursue();
        else if (patrolPoints.Length != 0)
            Patrol();
    }

    protected virtual void Pursue()
    {
        ///Moves enemy towards player. This is the most base version
        MoveTo(player.transform.position);
        currentTarget = player.transform.position;
    }

    protected virtual void Patrol()
    {
        ///Patrolling movement. Moves enemy between patrol points 
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % (patrolPoints.Length);
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
            currentTarget = currentPatrolPoint.position;
        }
        MoveTo(currentPatrolPoint.position);
    }

    protected void MoveTo(Vector3 position)
    {
        /* Basic movement. The bare bones of how the AI moves */
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);

        updateAnimations();
    }

    protected virtual void updateAnimations()
    {
        ///Updates visuals and animations
        if (anim)
            anim.SetBool("walking", move);

        if (currentTarget.x < transform.position.x)
            this.transform.localScale = new Vector3(-1.0f * startScale.x, transform.localScale.y);
        else
            this.transform.localScale = new Vector3(1.0f * startScale.x, transform.localScale.y);

        //Reseting rotations
        if (move)
        {
            if (this.tag.Contains("Boss"))
            {
                this.transform.rotation = startRotation;
            }
        }
    }

    public void changeBaseSpeed(float effect)
    {
        ///Changes the speed by multiplying the speeds by the effect
        ///Used for the various speed-affecting powers
        originalSpeed *= effect;
        currentSpeed *= effect;
    }

    protected void MoveAwayFrom(Vector3 position)
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, -step);
    }

    #region Helper Functions

    public IEnumerator Wait(float secs)
    {
        move = false;
        yield return new WaitForSeconds(secs);
        move = true;
    }

    protected float distFromPlayer()
    {
        return distFromTarget(player.transform.position);
    }

    protected float distFromTarget(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }
    #endregion
}
