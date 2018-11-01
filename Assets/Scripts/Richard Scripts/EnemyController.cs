using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float setFireTimer;
    public GameObject bullet;

    private GameObject player;
    private float fireTimer;
    private Health hp;

    private Vector3 center;

    private void Awake()
    {
        fireTimer = setFireTimer;
        hp = GetComponent<Health>();
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {
        fireTimer -= Time.deltaTime;

        // Timer to fire bullets on set intervals
        if (fireTimer <= 0f)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position));
            
            newBullet.tag = "Enemy Bullet";

            fireTimer = setFireTimer;
        }

        // Checks if enemy is dead and destorys them
        if (hp.dead())
        {
            Destroy(gameObject);
        }
    }

    public void startVortex(Vector3 c)
    {
        center = c;
    }
}
