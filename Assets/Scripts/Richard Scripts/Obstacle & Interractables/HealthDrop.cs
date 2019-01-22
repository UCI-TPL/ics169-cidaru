using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour {

    // Health amount to be restored to player
    public int healthRestore = 1;

    public GameObject hpEffect;

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
}
