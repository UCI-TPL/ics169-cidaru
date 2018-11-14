using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingController : MonoBehaviour {
    public enum BabyState
    {
        Normal,
        Succing,
        Rotato
    }
    
    public float rotationSpeed = 200f;

    protected Vector3 center;

    protected Rigidbody2D rb2d;
    protected BabyState currentState = BabyState.Normal;
    protected float radius = 0f;

    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentState = BabyState.Normal;
        radius = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState == BabyState.Succing)
        {
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentState = BabyState.Rotato;
        }
        else if (currentState == BabyState.Rotato)
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
        currentState = BabyState.Succing;
        transform.rotation = Quaternion.identity;
    }

    public void endVortex()
    {
        currentState = BabyState.Normal;
        
        transform.rotation = Quaternion.identity;
    }
}
