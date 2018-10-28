using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {

    public float setVortexTimer;

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
	}

    private void Blow()
    {
        foreach (GameObject thing in succedThings)
        {
            thing.GetComponent<Bullet>().endVortex();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (vortexState == VortexStates.Succ)
        {
            col.GetComponent<Bullet>().startVortex(transform.position);

            succedThings.Add(col.gameObject);
        }
    }


}
