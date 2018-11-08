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
    private AudioSource audio;
    private bool sound;

    public void Start()
    {
        bombArea.transform.localScale *= radius;
        audio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

        sound = false;
    }

    // Update is called once per frame
    void Update () {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f && !sound)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, enemyLayers);

            foreach (Collider2D hitCollider in hitColliders)
            {
                Instantiate(baby, hitCollider.transform.position, Quaternion.identity);

                Destroy(hitCollider.gameObject);
            }
            sound = true;
            sprite.enabled = false;
            areaSprite.enabled = false;
            audio.Play();

            Destroy(gameObject, 1.5f);
        }
	}
}
