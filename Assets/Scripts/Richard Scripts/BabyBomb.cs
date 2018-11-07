using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBomb : Bomb {
    public GameObject baby;

    public GameObject bombArea;

    public LayerMask enemyLayers;

    public float radius;

    public void Start()
    {
        bombArea.transform.localScale *= radius;
    }

    // Update is called once per frame
    void Update () {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, enemyLayers);

            foreach (Collider2D hitCollider in hitColliders)
            {
                Instantiate(baby, hitCollider.transform.position, Quaternion.identity);

                Destroy(hitCollider.gameObject);
            }

            Destroy(gameObject);
        }
	}
}
