using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEchoController : MonoBehaviour
{
    public GameObject echoObject;
    public float setSpawnTime = 0.01f;
    private SpriteRenderer sr;
    private float spawnTime;

    // Use this for initialization
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime <= 0)
        {
            GameObject echo = Instantiate(echoObject, transform.position, Quaternion.identity);

            echo.GetComponent<SpriteRenderer>().sprite = sr.sprite;
            echo.GetComponent<SpriteRenderer>().flipX = sr.flipX;

            Destroy(echo, 0.005f);
            spawnTime = setSpawnTime;
        }
        else if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
    }
}
