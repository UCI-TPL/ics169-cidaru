using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of Destroyable
public class ExplosiveObstacle : Destroyable {
    // Explosion object to be created (the actually damaging object)
    public GameObject explosion;

    // Damage value to be set for the explosion
    public int dmg = 1;

    // Radius value to be set for the explosion
    public float explosionRadius;

    // Object layers to be set for the explosion
    public LayerMask affectedLayers;

    public override void Death()
    {
        // Creates explosion and sets all its values
        Explosion newExplosion = Instantiate(explosion, transform.position, Quaternion.identity).GetComponent<Explosion>();
        newExplosion.radius = explosionRadius;
        newExplosion.dmg = dmg;
        newExplosion.affectedLayers = affectedLayers;

        base.Death();
    }
}
