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
        if (Time.timeScale != 0)
        {
            FaceMouse();
        }

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
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 mousePos = ray.GetPoint((0 - Camera.main.transform.position.z) / ray.direction.z);

        mousePos.z = 0f;

        gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
