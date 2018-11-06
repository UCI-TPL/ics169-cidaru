﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

    public GameObject vortexInside;
    
    private float vortexTimer;
    private List<Bullet> bullets = new List<Bullet>();
    private List<Enemy> enemies = new List<Enemy>();

    public enum VortexStates
    {
        Succ,
        Blow
    }

    private VortexStates vortexState = VortexStates.Succ;

    // Use this for initialization
    void Awake () {
        vortexTimer = setVortexTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Blow();
        }

        vortexTimer -= Time.deltaTime;

        if (vortexTimer <= 0)
        {
            Blow();
        }
	}

    private void Blow()
    {
        getBullets();

        foreach (Bullet b in bullets)
        {
            b.gameObject.transform.parent = null;
            b.gameObject.tag = "Vortex Bullet";

            b.endVortex();
        }

        foreach (Enemy e in enemies)
        {
            e.gameObject.transform.parent = null;

            e.endVortex();
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void getBullets()
    {
        int numOfChildren = vortexInside.transform.childCount;
        

        for (int i = 0; i < numOfChildren; i++)
        {
            if (vortexInside.transform.GetChild(i).GetComponent<Bullet>() != null)
            {
                bullets.Add(vortexInside.transform.GetChild(i).GetComponent<Bullet>());
            } else if (vortexInside.transform.GetChild(i).GetComponent<Enemy>() != null)
            {
                enemies.Add(vortexInside.transform.GetChild(i).GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {   
        if (vortexState == VortexStates.Succ && (col.tag == "Player Bullet" || col.tag == "Enemy Bullet" || col.tag == "Rotating Bullet" || col.tag == "Vortex Bullet"))
        {
            col.tag = "Rotating Bullet";

            col.GetComponent<Bullet>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (vortexState == VortexStates.Succ && col.gameObject.tag == "Enemy")
        {
            col.gameObject.layer = 11;

            col.gameObject.GetComponent<Enemy>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
    }

}
