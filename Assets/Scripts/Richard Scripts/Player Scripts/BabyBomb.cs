using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBomb : Bomb {
    // Object to change objects into
    public GameObject baby;

    // Area indication of how far the baby bomb will reach
    public GameObject bombArea;

    // Object layers in which the baby bomb will interact with
    public LayerMask enemyLayers;

    // Radius of the baby bomb
    public float radius;

    // Sprite indicator of how far the baby bomb will reach
    public SpriteRenderer areaSprite;

    // Sprite (diaper) of the baby bomb
    private SpriteRenderer sprite;

    // Audio of baby bomb
    private AudioSource audioSource;

    // Condition of whether the baby bomb has exploded yet
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
        // Countdown the baby bomb
        bombTimer -= Time.deltaTime;

        // If not exploded and timer expired, then explode
        if (bombTimer <= 0f && !exploded)
        {
            // Find all objects of specified layer in specified area
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, enemyLayers);

            // Loops through all the objects found and change all the desired ones into babies
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

            // Disables everything and starts audio noise
            exploded = true;
            sprite.enabled = false;
            areaSprite.enabled = false;
            audioSource.Play();

            // Destroy object after specified time
            Destroy(gameObject, 1.5f);
        }
	}
}
