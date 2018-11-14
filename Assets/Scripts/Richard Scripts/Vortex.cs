using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

    public GameObject vortexInside;
    
    protected float vortexTimer;
    protected List<Projectile> projectiles = new List<Projectile>();
    protected List<Enemy> enemies = new List<Enemy>();
    protected List<Destroyable> destroyables = new List<Destroyable>();

    public enum VortexStates
    {
        Succ,
        Blow
    }

    protected VortexStates vortexState = VortexStates.Succ;

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
        getSuccedObjects();

        foreach (Projectile p in projectiles)
        {
            p.gameObject.transform.parent = null;
            p.gameObject.tag = "Vortex Projectile";

            p.endVortex();
        }

        foreach (Enemy e in enemies)
        {
            e.gameObject.transform.parent = null;

            e.endVortex();
        }

        foreach (Destroyable d in destroyables)
        {
            d.gameObject.transform.parent = null;

            d.endVortex();
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void getSuccedObjects()
    {
        int numOfChildren = vortexInside.transform.childCount;
        

        for (int i = 0; i < numOfChildren; i++)
        {
            if (vortexInside.transform.GetChild(i).GetComponent<Projectile>() != null)
            {
                projectiles.Add(vortexInside.transform.GetChild(i).GetComponent<Projectile>());
            } else if (vortexInside.transform.GetChild(i).GetComponent<Enemy>() != null)
            {
                enemies.Add(vortexInside.transform.GetChild(i).GetComponent<Enemy>());
            } else if (vortexInside.transform.GetChild(i).GetComponent<Destroyable>() != null)
            {
                destroyables.Add(vortexInside.transform.GetChild(i).GetComponent<Destroyable>());
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {   
        if (vortexState == VortexStates.Succ && (col.tag == "Player Bullet" || col.tag == "Enemy Bullet" || col.tag == "Rotating Bullet" || col.tag == "Vortex Projectile"))
        {
            col.tag = "Rotating Bullet";

            col.GetComponent<Projectile>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
        
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (vortexState == VortexStates.Succ && col.gameObject.tag == "Enemy")
        {
            col.gameObject.layer = 11;

            col.gameObject.GetComponent<Enemy>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
        else if (vortexState == VortexStates.Succ && col.gameObject.tag == "Destroyable")
        {
            col.gameObject.layer = 11;

            col.gameObject.GetComponent<Destroyable>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
    }

}
