using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

    public GameObject vortexInside;

    public List<GameObject> succedThings = new List<GameObject>();

    private float vortexTimer;

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
        /*
        foreach (GameObject thing in succedThings)
        {
            thing.GetComponent<Bullet>().endVortex();
        }*/

        foreach (Transform child in vortexInside.transform)
        {
            child.parent = null;
            child.GetComponent<Bullet>().endVortex();
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (vortexState == VortexStates.Succ && col.tag != "Vortex")
        {
            col.GetComponent<Bullet>().startVortex(transform.position);

            col.transform.parent = vortexInside.transform;
            

            //succedThings.Add(col.gameObject);
        }
    }


}
