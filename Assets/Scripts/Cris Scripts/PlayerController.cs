using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    /* Controls player
    Based on:
    https://unity3d.com/learn/tutorials/projects/survival-shooter/player-character?playlist=17144
    */

    public float originalSpeed = 5f;
    public float setInvincibilityTimer = 2f;

    [HideInInspector]
    public float currentSpeed;
    //public GameObject gameOverPanel;

    private SpriteRenderer sprite;
    private PlayerHealth health;
    private float invincibilityTimer;
	private Animator anim;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator> ();
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
    }

    void FixedUpdate()
    {
        if (!health.dead())
            Move();

        /* USING GAMESTATE MANAGER TO CONTROL THIS
        #region Reset Health (When Dead)
        else
        {
            gameOverPanel.SetActive(true);
        }
        #endregion*/
    }

    void Move()
    {
        /* Basic free movement */

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

		if (x < 0) {
			this.transform.localScale = new Vector3 (-1.0f,
				transform.localScale.y);
		} else if (x > 0) {
			this.transform.localScale = new Vector3 (1.0f,
				transform.localScale.y);
		}

		if (x != 0 || y != 0) {
			anim.SetBool ("moving", true);
		} else {
			anim.SetBool ("moving", false);
		}

        Vector3 movement = new Vector3(x, y, 0);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        gameObject.transform.Translate(movement);
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