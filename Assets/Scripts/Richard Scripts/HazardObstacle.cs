using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObstacle : MonoBehaviour {

    public int dmg = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
    }
}
