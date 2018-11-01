using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject bullet;
    public GameObject vortexSpawner;

    public int setMaxAmmo = 10;

    public float reloadTime = 3;

    public int vortexManaCost = 20;

    private Transform gun;
    private int currentAmmo;
    private bool reloading;

    private Mana manaController;

    // Use this for initialization
    void Awake () {
        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)

        currentAmmo = setMaxAmmo;
        reloading = false;

        manaController = GetComponent<Mana>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            FaceMouse();

            if ((Input.GetKeyDown(KeyCode.R) && currentAmmo != setMaxAmmo && !reloading) || (currentAmmo == 0 && !reloading))
            {
                StartCoroutine(Reload());
            }

            // Player Shoot Code
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !reloading)
            {
                currentAmmo--;

                GameObject newBullet = Instantiate(bullet, transform.position, gun.rotation);
                newBullet.tag = "Player Bullet";
            }
            else if (Input.GetMouseButtonDown(1) && manaController.getCurrentMana() > vortexManaCost)
            {
                manaController.useMana(vortexManaCost);

                GameObject newVortex = Instantiate(vortexSpawner, transform.position, gun.rotation);
                newVortex.GetComponent<VortexSpawner>().setLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    private IEnumerator Reload()
    {
        reloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = setMaxAmmo;

        reloading = false;
    }

    public string getCurrentAmmoState()
    {
        if (reloading)
        {
            return "Reloading";
        } else
        {
            return "" + currentAmmo;
        }
    }
}
