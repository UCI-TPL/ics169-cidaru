using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float movementSpeed;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = transform.forward * movementSpeed * Time.deltaTime;
        rb2d.MovePosition(rb2d.position + move);
    }
}
