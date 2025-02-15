﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : Projectile {
    public float setFlameLengthTimer = 1f;

    public int tickAmount;
    public float burnTimer;

    private float flameLengthTimer;

    public override void Awake()
    {
        base.Awake();

        flameLengthTimer = setFlameLengthTimer;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        if (tag != "Rotating Bullet")
        {
            flameLengthTimer -= Time.deltaTime;

            if (flameLengthTimer <= 0)
            {
                Destroy(gameObject);
            }
        } else
        {
            flameLengthTimer = setFlameLengthTimer;
        }
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<StasisController>().startBurn(burnTimer, tickAmount);
        } else if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
