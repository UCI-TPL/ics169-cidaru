using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float speed = 1.0f;
    public float range = 1.0f;

    public GameObject[] guns;
    public GameObject bullet;
    public float fireRate = 1.0f;


    private float basex = 0.0f;
    private float basey = 0.0f;
    private float fireTimer = 0f;
    private bool fire = false;


    // Update is called once per frame
    void Start()
    {
        basex = transform.localPosition.x;
        basey = transform.localPosition.y;
    }

    void Update()
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            fire = true;
        }

        Vector3 position = transform.localPosition;
        float intervalx = Mathf.Sin(Time.time * (speed / range)) * range;
        float intervaly = Mathf.Cos(Time.time * (speed / range)) * range;

        position.x = basex + intervalx;
        position.y = basey + intervaly;

        if (fire)
        {
            foreach (GameObject gun in guns)
            {
                Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            }
            fire = false;
        }

        fireTimer += Time.deltaTime;
        transform.localPosition = position;
    }
}
