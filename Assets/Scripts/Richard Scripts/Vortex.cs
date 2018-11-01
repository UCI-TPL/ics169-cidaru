using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

    public GameObject vortexInside;
    
    private float vortexTimer;
    private List<Bullet> bullets = new List<Bullet>();

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

        foreach(Bullet b in bullets)
        {
            b.gameObject.transform.parent = null;
            b.gameObject.tag = "Vortex Bullet";

            b.endVortex();
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void getBullets()
    {
        int numOfChildren = vortexInside.transform.childCount;
        

        for (int i = 0; i < numOfChildren; i++)
        {
            bullets.Add(vortexInside.transform.GetChild(i).GetComponent<Bullet>());
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
            col.gameObject.layer = 10;

            col.gameObject.GetComponent<EnemyController>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
    }

}
