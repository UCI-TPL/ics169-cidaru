using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public int dmg = 1;
    public float radius;
    public LayerMask affectedLayers;

    // Use this for initialization
    void Start() {
        transform.localScale *= radius;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, affectedLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Contains("Weapon"))
                continue;

            hitCollider.GetComponent<Health>().TakeDamage(dmg);
        }

        Destroy(gameObject, 1f);
    }
}
