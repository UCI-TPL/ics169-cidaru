using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    public int dmg = 1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && tag == "Enemy Weapon") // Weapon hits player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }
    }

}
