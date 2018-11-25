using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject electric;
    public Ability vortex;

    public SpriteRenderer gunSprite;
    public Transform gunPoint;
    public float intialGunPointX;

    public int setMaxAmmo = 10;
    public float reloadTime = 3;

    //public int vortexManaCost = 20;

    private AmmoUI ammoUI;
    private GameObject reloadBarUI;
    private Slider reloadSlider;
    private GunTypes currentGun;
    private Transform gun;
    private AudioSource gunSFX;
    private int currentAmmo;
    private bool reloading;

    //private Mana manaController;

    // Use this for initialization
    void Awake () {
        ammoUI = GameObject.Find("Ammo Panel").GetComponent<AmmoUI>();
        reloadBarUI = GameObject.Find("Reload Bar");
        reloadSlider = reloadBarUI.GetComponent<Slider>();

        reloadSlider.maxValue = reloadTime;
        reloadBarUI.SetActive(false);

        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)
        gunSFX = gun.GetComponent<AudioSource>();
        currentGun = GunTypes.Thunder;

        currentAmmo = setMaxAmmo;
        reloading = false;

        initAbilityCharges();

        intialGunPointX = gunPoint.localPosition.x;
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale != 0)
        {
            FaceMouse();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                //changeGun();
            }

            if ((Input.GetKeyDown(KeyCode.R) && currentGun == GunTypes.Normal && currentAmmo != setMaxAmmo && !reloading) || (currentAmmo == 0 && !reloading))
            {
                StartCoroutine(Reload());
            }

            if (reloading)
            {
                reloadSlider.value = reloadSlider.value - Time.deltaTime;
            }

            // Player Shoot Code
            if (currentGun == GunTypes.Normal)
            {
                NormalShoot();
            } else if (currentGun == GunTypes.Fire)
            {
                FlameShoot();
            } else if (currentGun == GunTypes.Thunder)
            {
                ElectricShoot();
            }
            
            if (Input.GetMouseButtonDown(1) && vortex.isAbilityReady())//manaController.getCurrentMana() > vortexManaCost)
            {
                //manaController.useMana(vortexManaCost);

                vortex.PutOnCooldown();

                GameObject newVortex = Instantiate(vortex.abilityPrefab, gunPoint.position, gun.rotation);
                newVortex.GetComponent<VortexSpawner>().setLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (gunPoint.position.x - 0.8f >= mousePos.x || gunPoint.position.x + 0.8f <= mousePos.x ||
            gunPoint.position.y - 0.8f >= mousePos.y || gunPoint.position.y + 0.8f <= mousePos.y)
        {
            gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunPoint.position);

            if (gun.rotation.eulerAngles.z > 180)
            {
                gunSprite.flipY = true;
                gunPoint.localPosition = new Vector3(-intialGunPointX, gunPoint.localPosition.y);
            }
            else if (gun.rotation.eulerAngles.z < 180)
            {
                gunSprite.flipY = false;
                gunPoint.localPosition = new Vector3(intialGunPointX, gunPoint.localPosition.y);
            }
        }
    }

    private IEnumerator Reload()
    {
        reloadBarUI.SetActive(true);
        reloadSlider.value = reloadTime;
        reloading = true;

        ammoUI.clearAmmo();

        yield return new WaitForSeconds(reloadTime);

        reloadBarUI.SetActive(false);
        ammoUI.reloadAmmo();
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
            ammoUI.updateAmmo();
            currentAmmo--;

            gunSFX.Play();
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

    private void ElectricShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newElect = Instantiate(electric, gunPoint.position, gun.rotation);
            newElect.tag = "Player Bullet";
        }
    }

    private void changeGun()
    {
        if (currentGun == GunTypes.Normal)
        {
            currentGun = GunTypes.Fire;
        } else if (currentGun == GunTypes.Fire)
        {
            currentGun = GunTypes.Normal;
        }
    }

    private void initAbilityCharges()
    {
        vortex.initAbility();
    }
}
