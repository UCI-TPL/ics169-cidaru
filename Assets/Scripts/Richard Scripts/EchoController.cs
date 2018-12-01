using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoController : MonoBehaviour {

    public GameObject echoObject;
    public float setSpawnTime = 0.01f;

    private MovementModifier movement;
    private SpriteRenderer sr;
    private float spawnTime;

	// Use this for initialization
	void Awake () {
        movement = GetComponent<MovementModifier>();
        sr = GetComponent<SpriteRenderer>();
        spawnTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (movement.isSprinting() && spawnTime <= 0)
        {
            GameObject echo = Instantiate(echoObject, transform.position, Quaternion.identity);

            echo.GetComponent<SpriteRenderer>().sprite = sr.sprite;
            echo.GetComponent<SpriteRenderer>().flipX = sr.flipX;

            Destroy(echo, 0.25f);
            spawnTime = setSpawnTime;
        } else if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
	}
}
