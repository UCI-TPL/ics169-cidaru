using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    // Damage value of explosion to be inflicted
    public int dmg = 1;

    // Size of the explosion to check when activated
    public float radius;

    // Object layers that are damaged by the explosion
    public LayerMask affectedLayers;

    // Use this for initialization
    void Start() {
        // Sets the visual size of the explosion using the radius
        transform.localScale *= radius;

        // Finds all objects within specified layer in the area of explosion
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, affectedLayers);

        // Loops through each object and inflicts damage to the explosion
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Contains("Weapon"))
                continue;

            hitCollider.GetComponent<Health>().TakeDamage(dmg);
        }

        // Clears explosion from play field after specified time
        Destroy(gameObject, 1f);
    }
}
