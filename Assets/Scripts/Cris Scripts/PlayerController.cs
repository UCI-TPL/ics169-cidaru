﻿using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    /* Controls player
    Based on:
    https://unity3d.com/learn/tutorials/projects/survival-shooter/player-character?playlist=17144
    */

    public float originalSpeed = 5f;
    public float setInvincibilityTimer = 2f;

    //[HideInInspector]
    public float currentSpeed;
    //public GameObject gameOverPanel;

    private SpriteRenderer sprite;
    private PlayerHealth health;
    private float invincibilityTimer;
	private Animator anim;
    private Rigidbody2D rb2d;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        //gameOverPanel.SetActive(false);
        currentSpeed = originalSpeed;

        invincibilityTimer = 0f;
    }

    private void Update()
    {
        if (health.dead())
        {
            invincibilityTimer = 0f;
            //sprite.enabled = false;
            anim.SetBool("dead", true);
            GetComponent<SpriteRenderer>().sortingLayerName = "DrawOver";
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = false; //hides the gun while dying
        } else
        {
            anim.SetBool("dead", false);
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = true; //shows the gun
            // If timer is not over perform blinking effect else reenable sprite and vulnerability
            if (invincibilityTimer > 0f)
            {
                sprite.enabled = !sprite.enabled;
                invincibilityTimer -= Time.deltaTime;
            }
            else
            {
                sprite.enabled = true;
                health.setVulnerable();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                health.TakeDamage(2);
            }
        }
    }

    void FixedUpdate()
    {
        if (!health.dead() && !GameManager.gm.cameraPanning && !GameManager.gm.spawningRooms)
            Move();
    }

    void Move()
    {
        /* Basic free movement */

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x == 0)
            x = Input.GetAxisRaw("Horizontal");

        if (y == 0)
            y = Input.GetAxisRaw("Vertical");

        //print(x);
        //print(y);

        /*
		if (x < 0) {
            sprite.flipX = true;
		} else if (x > 0) {
            sprite.flipX = false;
		}*/   

		if (x != 0 || y != 0) {
			anim.SetBool ("moving", true);
		} else {
			anim.SetBool ("moving", false);
		}

        Vector3 movement = new Vector3(x, y, 0);

        movement = movement.normalized * currentSpeed * Time.deltaTime;
        rb2d.MovePosition(transform.position + movement);
        //gameObject.transform.Translate(movement);
    }

    public void startInvincibility()
    {
        // Checks if player is dead first, if so disable sprite else start invincibility
        if (health.dead())
        {
            sprite.enabled = false;
        } else
        {
            invincibilityTimer = setInvincibilityTimer;
        }
    }
}