using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {
    public enum DestroyableRotatingState
    {
        Normal,
        Succing,
        Rotato
    }
    
    public float rotationSpeed = 200f;

    protected Vector3 center;

    protected Rigidbody2D rb2d;
    protected DestroyableRotatingState currentState = DestroyableRotatingState.Normal;
    protected float radius = 0f;

    private Health hp;

	// Use this for initialization
	void Awake () {
        hp = GetComponent<Health>();

        rb2d = GetComponent<Rigidbody2D>();
        currentState = DestroyableRotatingState.Normal;
        radius = 0f;
    }
	
	// Update is called once per frame
	void Update () {
		if (hp.dead())
        {
            Death();
        }

        if (currentState == DestroyableRotatingState.Succing)
        {
            rb2d.freezeRotation = false;
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentState = DestroyableRotatingState.Rotato;
        }
        else if (currentState == DestroyableRotatingState.Rotato)
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
        currentState = DestroyableRotatingState.Succing;
        rb2d.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.identity;
    }

    public void endVortex()
    {
        currentState = DestroyableRotatingState.Normal;

        gameObject.layer = 0;

        transform.rotation = Quaternion.identity;

        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
