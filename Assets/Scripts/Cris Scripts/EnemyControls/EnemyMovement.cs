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

    #region Visual Stuff
    private Animator anim;
    private Vector3 startScale;
    private Quaternion startRotation;
    #endregion

    #region Pathfinding Stuff
    //private NodeManager nodeManager;
    //public Node currentNode;
    //public Node nextNode;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        startScale = transform.localScale;
        startRotation = transform.rotation;
        currentSpeed = originalSpeed;
        lastSpeed = currentSpeed;

        move = true;
        player = GameObject.Find("Player");

        charged = false;
        currentTarget = player.transform.position;

        //AStar-based stuff
        //nodeManager = transform.parent.parent.Find("Nodes").GetComponent<NodeManager>();
        //currentNode = nodeManager.getNearestNode(this.transform.position);

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
            //Debug.Log("Pursuing but like w/o charge");
            //Debug.Log("Target: " + player.transform.position);
            Move(player.transform.position);
            currentTarget = player.transform.position;
        }
    }

    #region Charging
    private void ChargeBasedMovement()
    {
        if (playerCloserThan(chargeDistance) && !charged)
        {
            Vector3 dir = Vector3.Normalize((transform.position - currentTarget));
            Charge(currentTarget + dir*10);
            Debug.DrawLine(transform.position, currentTarget + dir * 3, Color.red);
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
    public void Move(Vector3 position)
    {
        /* Basic movementthe base bones of how the AI moves */
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        //GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, position, step));

        //Vector3 nextPosition = nodeManager.getNextNodePosition(this.transform.position, position);//getNextNode(position);
        //Debug.Log("Next Node: " + nextPosition);
        //transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, step);

        //Node nearestPositionNode = nodeManager.getNearestNode(position);
        //nextNode = nodeManager.getNextNode(currentNode, nearestPositionNode);//getNextNode(position);
        //Debug.Log("Next Node: " + nextNode);
        //transform.position = Vector3.MoveTowards(this.transform.position, nextNode.transform.position, step);
        //if (nextNode.transform.position == this.transform.position)
        //    currentNode = nextNode;
    }

    public IEnumerator Wait(float secs)
    {
        move = false;
        //Debug.Log("Waiting...");
        this.transform.Rotate(new Vector3(startRotation.x, startRotation.y, startRotation.z + 20));
        yield return new WaitForSeconds(secs);
        //Debug.Log("Done waiting!");
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

        //Reseting rotations
        if (move)
        {
            if (this.tag.Contains("Boss"))
            {
                this.transform.rotation = startRotation;
            }
            if (chargeDistance != 0)
                this.transform.rotation = startRotation;
        }
    }

    public void changeBaseSpeed(float effect)
    {
        originalSpeed *= effect;
        currentSpeed *= effect;
        lastSpeed *= effect;
    }
}
