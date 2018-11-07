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

    public EnemyAttack attackStyle;
    public EnemyMovement movement;
    public int aggroRange;

    protected GameObject player;
    private Health hp;

    public float rotationSpeed = 200f;

    private Rigidbody2D rb2d;
    private EnemyState currentState = EnemyState.Normal;
    private Vector3 center;
    public float radius = 0f;
    [HideInInspector]
    public bool aggressing = false;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = GetComponent<Health>();

        rb2d = GetComponent<Rigidbody2D>();
        currentState = EnemyState.Normal;
        radius = 0f;
	}
	
	void FixedUpdate () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(player.transform.position, transform.position));
        aggressing = hit.collider.tag == "Player" && Vector3.Distance(player.transform.position, transform.position) <= aggroRange && currentState == EnemyState.Normal;

        if (aggressing)
        {
            attackStyle.Attack();
        }
        checkDeath();
    }

    public void Update()
    {
        if (currentState == EnemyState.Succing)
        {
            rb2d.freezeRotation = false;
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentState = EnemyState.Rotato;
        } else if (currentState == EnemyState.Rotato)
        {
            radius = Vector3.Distance(transform.position, center);
            if (radius >= 0.03f)
            {
                radius -= 0.01f;
            }

            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.deltaTime);

            rb2d.MovePosition(center + radius * new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180)));
        }
    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentState = EnemyState.Succing;
        transform.rotation = Quaternion.identity;

        movement.enabled = false;
    }

    public void endVortex()
    {
        currentState = EnemyState.Normal;

        movement.enabled = true;

        gameObject.layer = 0;

        transform.rotation = Quaternion.identity;

        rb2d.freezeRotation = true;
    }

    private void checkDeath()
    {
        // Checks if enemy is dead and destroys them
        if (hp.dead())
        {
            Destroy(gameObject);
        }
    }
}
