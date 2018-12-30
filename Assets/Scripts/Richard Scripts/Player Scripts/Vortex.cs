using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    // Timer til vortex expires
    public float setVortexTimer;

    // Gameobject to parent all objects that enter the vortex
    public GameObject vortexInside;
    
    // Current timer til vortex expires
    protected float vortexTimer;

    // Lists containing all objects that are within the vortex
    protected List<Projectile> projectiles = new List<Projectile>();
    protected List<Enemy> enemies = new List<Enemy>();
    protected List<MeleeWeapon> weapons = new List<MeleeWeapon>();
    protected List<Destroyable> destroyables = new List<Destroyable>();
    protected List<RotatingController> babies = new List<RotatingController>();

    // States a vortex object can be in
    public enum VortexStates
    {
        Succ,
        Blow
    }

    // Current state in which the vortex is in
    protected VortexStates vortexState = VortexStates.Succ;

    // Use this for initialization
    void Awake () {
        // Sets the timer to max
        vortexTimer = setVortexTimer;
	}
	
	// Update is called once per frame
	void Update () {

        // When Right Bumper or Right Mouse Click are pressed while vortex is active then explode
        if (Input.GetButtonDown("Right Bumper") || Input.GetMouseButtonDown(1))
            Blow();

        // Countdown the vortex timer
        vortexTimer -= Time.deltaTime;

        // When timer expires, explode the vortex
        if (vortexTimer <= 0)
            Blow();
	}

    // Ends the vortex and sends objects out of the vortex
    private void Blow()
    {
        // Finds all the objects that within the vortex
        getSuccedObjects();

        // Sets each projectile to be able to damage everything and removes them from the vortex
        foreach (Projectile p in projectiles)
        {
            // Removes the projectile from the vortex parent
            p.gameObject.transform.parent = null;

            // Sets the projectile tag to allow it to damage everything
            p.gameObject.tag = "Vortex Projectile";

            // Sets the trail color to be a new color to represent this
            if (p.gameObject.GetComponent<TrailRenderer>())
                p.gameObject.GetComponent<TrailRenderer>().startColor = Color.red;

            // Ends the spinning and returns the projectile to moving normally
            p.endVortex();
        }

        // Sets each enemy back to normal
        foreach (Enemy e in enemies)
        {
            // Removes the enemy from the vortex parent
            e.gameObject.transform.parent = null;

            // Ends the spinning and returns the enemy to moving normally
            e.endVortex();
        }

        // Sets each weapon back to normal
        foreach (MeleeWeapon w in weapons)
        {
            // Removes the melee weapon from the vortex parent
            w.gameObject.transform.parent = null;

            // Ends the spinning and returns the enemy to moving normally
            w.endVortex();
        }

        // Sets each destroayble object back to normal
        foreach (Destroyable d in destroyables)
        {
            // Removes the destroyable object from the vortex parent
            d.gameObject.transform.parent = null;

            // Ends the spinning and returns the destroyable object back to default
            d.endVortex();
        }

        // Sets each rotating object back to normal
        foreach (RotatingController b in babies)
        {
            // Removes the rotateable object from the vortex parent
            b.gameObject.transform.parent = null;
            
            // Ends the spinning and returns the rotateable object back to default
            b.endVortex();
        }

        // Destroys the main vortex object
        Destroy(gameObject.transform.parent.gameObject);
    }

    // Finds all the objects within the 
    private void getSuccedObjects()
    {
        // Counts the number of objects within the vortex
        int numOfChildren = vortexInside.transform.childCount;
        
        // Loops through all objects and place them in their respectable catagory
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
        // If object touches is projectile set it to be rotating and start rotation
        // If object touches is baby start rotating normally
        if (vortexState == VortexStates.Succ && (col.tag == "Player Bullet" || col.tag == "Enemy Bullet" || col.tag == "Rotating Bullet" || col.tag == "Vortex Projectile"))
        {
            col.tag = "Rotating Bullet";

            // Starts rotation of projectile
            col.GetComponent<Projectile>().startVortex(transform.position);

            // Sets object to be "within" vortex
            col.transform.parent = vortexInside.transform;
        }
        else if (vortexState == VortexStates.Succ && col.tag == "Baby")
        {
            // Starts rotation of projectile
            col.GetComponent<RotatingController>().startVortex(transform.position);

            // Sets object to be "within" vortex
            col.transform.parent = vortexInside.transform;
        }

    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        // If object touches is an enemy and vortexable, start rotating
        // If object touches is a destroyable, start rotating the destroyable as normal
        if (vortexState == VortexStates.Succ && col.gameObject.tag.Contains("Enemy")
            && col.gameObject.GetComponent<Enemy>().vortex)
        {
            // Layer change to change how collision works
            col.gameObject.layer = 11;

            // Sets object to be "within" vortex
            col.transform.parent = vortexInside.transform;

            // Starts rotation of enemy object
            if (col.gameObject.GetComponent<Enemy>() &&
                col.gameObject.GetComponent<Enemy>().vortex)
            {
                col.gameObject.GetComponent<Enemy>().startVortex(transform.position);
            }

        }
        else if (vortexState == VortexStates.Succ && col.gameObject.tag == "Destroyable")
        {
            // Layer change to change how collision works
            col.gameObject.layer = 11;

            // Starts rotation of destroyable object
            col.gameObject.GetComponent<Destroyable>().startVortex(transform.position);

            // Sets object to be "within" vortex
            col.transform.parent = vortexInside.transform;
        }
    }

}
