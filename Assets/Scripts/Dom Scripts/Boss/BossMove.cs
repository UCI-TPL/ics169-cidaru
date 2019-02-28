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
    public List<GameObject> bulletBelt;
    public int amountToPool;


    private float basex = 0.0f;
    private float basey = 0.0f;
    private float fireTimer = 0f;
    private bool fire = false;


    // Update is called once per frame
    void Start()
    {
        basex = transform.localPosition.x;
        basey = transform.localPosition.y;

        bulletBelt = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject o = (GameObject)Instantiate(bullet);
            o.SetActive(false);
            bulletBelt.Add(o);
        }
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
                GameObject newBullet = getBullet();
                if (newBullet != null)
                {
                    newBullet.transform.position = gun.transform.position;
                    newBullet.transform.rotation = gun.transform.rotation;
                    newBullet.SetActive(true);
                }
            }

            fire = false;
        }

        fireTimer += Time.deltaTime;
        transform.localPosition = position;
    }

    public GameObject getBullet()
    {
        for (int i = 0; i < bulletBelt.Count; i++)
        {
            if (!bulletBelt[i].activeInHierarchy)
            {
                return bulletBelt[i];
            }
        }
        return null;
    }
}
