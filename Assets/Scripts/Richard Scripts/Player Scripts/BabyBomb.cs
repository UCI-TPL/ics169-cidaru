using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyBomb : Bomb {
    // Object to change objects into
    public GameObject baby;

    public GameObject miniTrojanShooter;

    public GameObject sprout;

    // Object layers in which the baby bomb will interact with
    public LayerMask interactableLayer;

    // Radius of the baby bomb
    public float radius;

    public Image fillImage;

    // Sprite (diaper) of the baby bomb
    private SpriteRenderer sprite;

    // Audio of baby bomb
    private AudioSource audioSource;

    // Particle Emitter of baby bomb
    private ParticleSystem emitter;
    public float emissionTimer = 0.1f;

    // Condition of whether the baby bomb has exploded yet
    private bool exploded;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        emitter = GetComponent<ParticleSystem>();
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
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, interactableLayer);

            // Loops through all the objects found and change all the desired ones into babies
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag.Contains("Enemy") && hitCollider.gameObject.GetComponent<Enemy>() &&
                    !hitCollider.gameObject.GetComponent<Enemy>().babyBomb && !hitCollider.gameObject.GetComponent<Enemy>().bigBabyBomb)
                    continue;
                if (hitCollider.gameObject.tag.Contains("Weapon"))
                    continue;
                if (hitCollider.gameObject.tag.Contains("Turret"))
                    continue;
                if (hitCollider.gameObject.layer == 14 && hitCollider.gameObject.tag != "Tree")
                    continue;

                if (hitCollider.gameObject.tag == "Tree")
                {
                    Instantiate(sprout, hitCollider.transform.position - new Vector3(0, 1), Quaternion.identity);
                } 
                else if (hitCollider.gameObject.tag == "Tutorial")
                {
                    Instantiate(baby, hitCollider.transform.position, Quaternion.identity);
                }
                else if (hitCollider.gameObject.GetComponent<Enemy>().bigBabyBomb)
                {
                    GameObject miniBoy = Instantiate(miniTrojanShooter, hitCollider.transform.position, Quaternion.identity);

                    hitCollider.transform.parent.parent.GetComponentInChildren<Door>().addEnemy(miniBoy);
                }
                else
                {
                    Instantiate(baby, hitCollider.transform.position, Quaternion.identity);
                }

                if (hitCollider.gameObject.tag == "Tree")
                    Destroy(hitCollider.transform.parent.gameObject);
                else
                    Destroy(hitCollider.gameObject);
            }

            // Disables everything and starts audio noise
            fillImage.enabled = false;
            exploded = true;
            sprite.enabled = false;
            audioSource.Play();

            // Shoot diapers out
            StartCoroutine("emit");


            // Destroy object after specified time
            Destroy(gameObject, 1.5f);
        }
	}

    private IEnumerator emit()
    {
        emitter.Play();
        yield return new WaitForSeconds(emissionTimer);
        emitter.Stop();
    }
}
