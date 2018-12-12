using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

    public GameObject vortexInside;
    
    protected float vortexTimer;
    protected List<Projectile> projectiles = new List<Projectile>();
    protected List<Enemy> enemies = new List<Enemy>();
    protected List<MeleeWeapon> weapons = new List<MeleeWeapon>();
    protected List<Destroyable> destroyables = new List<Destroyable>();
    protected List<RotatingController> babies = new List<RotatingController>();

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

        foreach (MeleeWeapon w in weapons)
        {
            w.gameObject.transform.parent = null;
            w.endVortex();
        }

        foreach (Destroyable d in destroyables)
        {
            d.gameObject.transform.parent = null;

            d.endVortex();
        }

        foreach (RotatingController b in babies)
        {
            b.gameObject.transform.parent = null;

            b.endVortex();
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
            } else if (vortexInside.transform.GetChild(i).GetComponent<RotatingController>() != null)
            {
                babies.Add(vortexInside.transform.GetChild(i).GetComponent<RotatingController>());
            } else if (vortexInside.transform.GetChild(i).GetComponent<MeleeWeapon>() != null)
            {
                weapons.Add(vortexInside.transform.GetChild(i).GetComponent<MeleeWeapon>());
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
        else if (vortexState == VortexStates.Succ && col.tag == "Baby")
        {
            col.GetComponent<RotatingController>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }

    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (vortexState == VortexStates.Succ && col.gameObject.tag.Contains("Enemy")
            && col.gameObject.GetComponent<Enemy>().vortex)
        {
            col.gameObject.layer = 11;
            col.transform.parent = vortexInside.transform;

            if (col.gameObject.GetComponent<Enemy>() &&
                col.gameObject.GetComponent<Enemy>().vortex)
            {
                col.gameObject.GetComponent<Enemy>().startVortex(transform.position);
            }

        }
        else if (vortexState == VortexStates.Succ && col.gameObject.tag == "Destroyable")
        {
            col.gameObject.layer = 11;

            col.gameObject.GetComponent<Destroyable>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
        }
    }

}
