using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObstacle : MonoBehaviour {

    // Damage to be inflicted
    public int dmg = 1;

    // On player contact, deal damage to the player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
    }
}
