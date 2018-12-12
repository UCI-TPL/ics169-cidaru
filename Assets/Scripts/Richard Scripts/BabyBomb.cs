﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBomb : Bomb {
    public GameObject baby;

    public GameObject bombArea;

    public LayerMask enemyLayers;

    public float radius;

    public SpriteRenderer areaSprite;

    private SpriteRenderer sprite;
    private AudioSource audioSource;
    private bool exploded;

    public void Start()
    {
        bombArea.transform.localScale *= radius;
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        exploded = false;
    }

    // Update is called once per frame
    void Update () {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f && !exploded)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, enemyLayers);

            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag.Contains("Enemy") && hitCollider.gameObject.GetComponent<Enemy>() && !hitCollider.gameObject.GetComponent<Enemy>().babyBomb)
                    continue;
                if (hitCollider.gameObject.tag.Contains("Weapon"))
                    continue;
                if (hitCollider.gameObject.tag.Contains("Turret"))
                    continue;

                Instantiate(baby, hitCollider.transform.position, Quaternion.identity);

                Destroy(hitCollider.gameObject);
            }

            exploded = true;
            sprite.enabled = false;
            areaSprite.enabled = false;
            audioSource.Play();

            Destroy(gameObject, 1.5f);
        }
	}
}
