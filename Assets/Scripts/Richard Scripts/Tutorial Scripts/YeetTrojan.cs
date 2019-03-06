using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetTrojan : MonoBehaviour
{
    public float movementSpeed;

    public Transform startPortal;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = transform.up * movementSpeed * Time.deltaTime;

        rb2d.MovePosition(rb2d.position + move);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Yeet Portal")
            transform.position = startPortal.position;
    }
}
