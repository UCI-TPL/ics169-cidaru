using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour {

    // Health amount to be restored to player
    public int healthRestore = 1;

    public GameObject hpEffect;

    public float attractionSpeed;

    public GameObject particleTrail;

    private Transform player;
    private bool attracted = false;
    private float maxAttractionSpeed;
    private float t;

    private void Awake()
    {
        attracted = false;
        maxAttractionSpeed = attractionSpeed + 5;
        t = 0;
    }

    private void Update()
    {
        if (attracted)
        {
            while (t < 1)
            {
                t += Time.deltaTime / 100f;

                attractionSpeed = Mathf.Lerp(attractionSpeed, maxAttractionSpeed, t);
            }

            transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.position, attractionSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On player contact perform check if player is max health
        // If not, restore players health and destroy the object
        if (collision.tag == "Player")
        {
            if (!collision.GetComponent<PlayerHealth>().isMaxHealth())
            {
                collision.GetComponent<PlayerHealth>().Heal(healthRestore);
                Instantiate(hpEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void StartAttraction(Transform p)
    {
        particleTrail.SetActive(true);
        attracted = true;
        player = p;
    }
}
