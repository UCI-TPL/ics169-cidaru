using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyBomb : Bomb {
    // Object to change objects into
    public GameObject baby;

    // Object layers in which the baby bomb will interact with
    public LayerMask enemyLayers;

    // Radius of the baby bomb
    public float radius;

    public Image fillImage;

    // Sprite (diaper) of the baby bomb
    private SpriteRenderer sprite;

    // Audio of baby bomb
    private AudioSource audioSource;

    // Condition of whether the baby bomb has exploded yet
    private bool exploded;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        exploded = false;
    }

    // Update is called once per frame
    void Update () {
        // Countdown the baby bomb
        bombTimer -= Time.deltaTime;
        fillImage.fillAmount = (setBombTimer - bombTimer) / setBombTimer;

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

                if (hitCollider.gameObject.tag == "Tutorial")
                    hitCollider.GetComponent<TutorialTrojan>().babyTutorialTree();

                Instantiate(baby, hitCollider.transform.position, Quaternion.identity);

                Destroy(hitCollider.gameObject);
            }

            // Disables everything and starts audio noise
            fillImage.enabled = false;
            exploded = true;
            sprite.enabled = false;
            audioSource.Play();

            // Destroy object after specified time
            Destroy(gameObject, 1.5f);
        }
	}
}
