using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : Destroyable {
    public GameObject explosion;
    public int dmg = 1;
    public float explosionRadius;
    public LayerMask affectedLayers;

    public override void Death()
    {
        Explosion newExplosion = Instantiate(explosion, transform.position, Quaternion.identity).GetComponent<Explosion>();
        newExplosion.radius = explosionRadius;
        newExplosion.dmg = dmg;
        newExplosion.affectedLayers = affectedLayers;

        base.Death();
    }
}
