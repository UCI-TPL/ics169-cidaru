using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject bullet;
    public GameObject vortexSpawner;

    public int vortexManaCost;

    private Transform gun;

    private Mana manaController;

    // Use this for initialization
    void Awake () {
        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)

        manaController = GetComponent<Mana>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            FaceMouse();
        }

        // Player Shoot Code
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, gun.rotation);
            newBullet.tag = "Player Bullet";
        } else if (Input.GetMouseButtonDown(1) && manaController.getCurrentMana() > vortexManaCost)
        {
            manaController.useMana(vortexManaCost);
            GameObject newVortex = Instantiate(vortexSpawner, transform.position, gun.rotation);
            newVortex.GetComponent<VortexSpawner>().setLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
