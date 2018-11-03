using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    public int dmg = 1;
    public int aggroRange;
    public float waitTime = 0f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && tag == "Enemy Weapon") // Weapon hits player
        {
            collision.GetComponent<Health>().TakeDamage(dmg);
            StartCoroutine(GetComponentInParent<EnemyMovement>().Wait(waitTime));
        }
    }

}
