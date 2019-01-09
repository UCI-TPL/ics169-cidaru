using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {
    public enum EnemyState
    {
        Normal,
        Succing,
        Rotato
    }

    [Header("Attributes")]
    public EnemyAttack attackStyle; // The script that dictates the enemy's attack patterns
    public EnemyMovement movement; // The script that dictates the enemy's movement
    [HideInInspector]
    public bool aggressing; // whether or not the enemy is going after the player

    private GameObject player; 
    private Health hp;

    [Header("Affected By")] // All powers this enemy can be affected by
    public bool babyBomb;
    public bool speedBubbles;
    public bool vortex;

    [Header("Vortex Stuff")] // Stuff used for vortex-specific effects
    public float rotationSpeed = 200f;
    public float radius = 0f;

    private Rigidbody2D rb2d;
    private EnemyState currentState = EnemyState.Normal;
    private Vector3 center;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = GetComponent<Health>();

        rb2d = GetComponent<Rigidbody2D>();
        currentState = EnemyState.Normal;
        radius = 0f;
        aggressing = false;
	}
	
	void FixedUpdate () {
        checkDeath(); // Make sure they're not dead before doing other stuff

        /// Do things based on what the currentState is
        switch (currentState)
        {
            case EnemyState.Normal:
                handleNormal();
                break;
            case EnemyState.Rotato:
                handleRotato();
                break;
            case EnemyState.Succing:
                handleSucc();
                break;
            default:
                handleNormal();
                break;
        }
    }

    private void handleNormal()
    {
        /// Normal enemy behavior
        rb2d.velocity = new Vector2(0, 0); //bc movement isn't based on velocity... yet.

        if (!aggressing)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                player.transform.position - transform.position,
                Vector3.Distance(player.transform.position, transform.position),
                LayerMask.GetMask("Default")); //Only checks on the layer with colliders/obstacles

            //Debug.DrawLine(transform.position, (Vector3)hit.point);
            aggressing = hit.collider == null; // if nothing was hit, then the path to the player is obstacle-free
        }
        else
        {
            attackStyle.Attack();
            movement.Move(aggressing);
        }
    }

    private void handleSucc()
    {
        /// The stuff that needs to happen when the state is Succ
        rb2d.freezeRotation = false;
        Vector2 difference = transform.position - center;

        float angle = Vector2.Angle(Vector2.right, difference);

        if (transform.position.y - center.y < 0)
        {
            angle = -angle;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);

        currentState = EnemyState.Rotato;
    }

    private void handleRotato()
    {
        /// The stuff that needs to happen when the state is Rotato
        radius = Vector3.Distance(transform.position, center);
        if (radius >= 0.03f)
        {
            radius -= 0.01f;
        }

        rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.deltaTime);

        rb2d.MovePosition(center + radius * new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180)));
    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentState = EnemyState.Succing;
        transform.rotation = Quaternion.identity;

        movement.enabled = false;

        if (typeof(MeleeEnemy) == attackStyle.GetType())
        {
            MeleeEnemy attack = (MeleeEnemy) attackStyle;
            attack.weapon.GetComponent<MeleeWeapon>().startVortex(c);
            attack.makeVulnerable();
        }

        attackStyle.enabled = false;
    }

    public void endVortex()
    {
        currentState = EnemyState.Normal;

        movement.enabled = true;

        gameObject.layer = 12;

        transform.rotation = Quaternion.identity;

        rb2d.freezeRotation = true;

        if (typeof(MeleeEnemy) == attackStyle.GetType())
        {
            MeleeEnemy attack = (MeleeEnemy)attackStyle;
            attack.weapon.GetComponent<MeleeWeapon>().endVortex();
        }

        attackStyle.enabled = true;
    }

    private void checkDeath()
    {
        // Checks if enemy is dead and destroys them
        if (hp.currentHealth <= 0)
        {
            if (GetComponent<DropController>())
                GetComponent<DropController>().calculateDrop();
            Destroy(gameObject);
        }
    }
}
