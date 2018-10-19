using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject bullet;

    private Transform gun;

    // Use this for initialization
    void Awake () {
        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)
	}
	
	// Update is called once per frame
	void Update () {
        FaceMouse();

        // Player Shoot Code
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, gun.rotation);
            newBullet.tag = "Player Bullet";
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
