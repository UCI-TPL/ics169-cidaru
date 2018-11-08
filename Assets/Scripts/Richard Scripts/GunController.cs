using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public enum GunTypes
    {
        Normal,
        Fire,
        Ice,
        Thunder,
    }

    public GameObject bullet;
    public GameObject flame;
    public GameObject vortexSpawner;

    public SpriteRenderer gunSprite;
    public Transform gunPoint;

    public int setMaxAmmo = 10;
    public float reloadTime = 3;

    public int vortexManaCost = 20;

    private GunTypes currentGun;
    private Transform gun;
    private int currentAmmo;
    private bool reloading;

    private Mana manaController;

    // Use this for initialization
    void Awake () {
        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)
        currentGun = GunTypes.Normal;

        currentAmmo = setMaxAmmo;
        reloading = false;

        manaController = GetComponent<Mana>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            FaceMouse();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentGun = GunTypes.Normal;
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentGun = GunTypes.Fire;
            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentGun = GunTypes.Ice;
            } else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentGun = GunTypes.Thunder;
            }

            if ((Input.GetKeyDown(KeyCode.R) && currentGun == GunTypes.Normal && currentAmmo != setMaxAmmo && !reloading) || (currentAmmo == 0 && !reloading))
            {
                StartCoroutine(Reload());
            }

            // Player Shoot Code
            if (currentGun == GunTypes.Normal)
            {
                NormalShoot();
            } else if (currentGun == GunTypes.Fire)
            {
                FlameShoot();
            }
            
            if (Input.GetMouseButtonDown(1) && manaController.getCurrentMana() > vortexManaCost)
            {
                manaController.useMana(vortexManaCost);

                GameObject newVortex = Instantiate(vortexSpawner, gunPoint.position, gun.rotation);
                newVortex.GetComponent<VortexSpawner>().setLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        print(mousePos);

        print("gun: " + gunPoint.position);

        if (gunPoint.position.x - 0.8f >= mousePos.x || gunPoint.position.x + 0.8f <= mousePos.x ||
            gunPoint.position.y - 0.8f >= mousePos.y || gunPoint.position.y + 0.8f <= mousePos.y)
        {
            gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunPoint.position);

            if (gun.rotation.eulerAngles.z > 180)
            {
                gunSprite.flipY = true;
            }
            else if (gun.rotation.eulerAngles.z < 180)
            {
                gunSprite.flipY = false;
            }
        }
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

    private void NormalShoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !reloading)
        {
            currentAmmo--;

            GameObject newBullet = Instantiate(bullet, gunPoint.position, gun.rotation);
            newBullet.tag = "Player Bullet";
        }
    }

    private void FlameShoot()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject newFlame = Instantiate(flame, gunPoint.position, gun.rotation);
            newFlame.tag = "Player Bullet";
        }
    }
}
