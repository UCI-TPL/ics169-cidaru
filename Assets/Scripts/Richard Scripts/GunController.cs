using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

    #region Old Elemental Variables
    /*
    public enum GunTypes
    {
        Normal,
        Fire,
        Ice,
        Thunder,
    }

    public GameObject flame;
    public GameObject electric;
    
    public Weapon flamethrower;
    public Weapon stunGun;

    private GunTypes currentGun;
    */
    #endregion Old Elemental Variables

    #region Pre-Scriptable Object Variables
    //public GameObject bullet;
    //public float reloadTime = 3;

    //private GameObject reloadBarUI;
    //private AmmoUI ammoUI;

    #endregion Pre-Scriptable Object Variables


    public Weapon normalGun;

    public Ability vortex;

    public SpriteRenderer gunSprite;
    public Transform gunPoint;

    public float setFireRate = 0.3f;

    private float intialGunPointX;
    
    private Transform gun;
    private AudioSource gunSFX;
    private float shootTimer;
    private Slider reloadSlider;
    private bool reloading;
    private float reloadTime;

    // Use this for initialization
    void Awake () {

        #region Pre-Scriptable Object Init
        //ammoUI = GameObject.Find("Ammo Panel").GetComponent<AmmoUI>();
        //reloadBarUI = GameObject.Find("Reload Bar");

        //reloadSlider.maxValue = reloadTime;
        //reloadBarUI.SetActive(false);
        #endregion Pre-Scriptable Object Init

        #region Old Elemental Weapon Init
        //currentGun = GunTypes.Normal; Old Elemental Init
        //getCurrentWeapon().ActivateWeapon();
        #endregion Old Elemental Weapon Init

        gun = transform.GetChild(0); // Grabs first child object and grabs the position (In this case the gun)
        gunSFX = gun.GetComponent<AudioSource>();

        shootTimer = 0f;

        reloadSlider = GameObject.Find("Reload Bar").GetComponent<Slider>();
        reloading = false;

        initAbilityCharges();
        initWeapons();

        normalGun.ActivateWeapon();
        
        intialGunPointX = gunPoint.localPosition.x;

        if (!PlayerPrefs.HasKey("Mouse"))
            PlayerPrefs.SetInt("Mouse", 1);

    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale != 0 && !GameManager.gm.cameraPanning && !GameManager.gm.spawningRooms)
        {
            if (PlayerPrefs.GetInt("Mouse") == 0)
                FaceStick();
            else
                FaceMouse();

            #region Old Elemental Weapon Switch
            //if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !reloading)
            //{
            //    getCurrentWeapon().DeactiveWeapon();
            //    currentGun = nextWeapon();
            //    getCurrentWeapon().ActivateWeapon();
            //}
            //else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && !reloading)
            //{
            //    getCurrentWeapon().DeactiveWeapon();
            //    currentGun = previousWeapon();
            //    getCurrentWeapon().ActivateWeapon();
            //}
            #endregion Old Elemental Weapon Switch

            #region Old Elemental Weapon Reload
            //if ((Input.GetKeyDown(KeyCode.R) && getCurrentWeapon().CheckReloadable() && !reloading) || (getCurrentWeapon().CheckAutoReload() && !reloading))
            //{
            //    reloading = true;
            //    getCurrentWeapon().Reload(this);
            //    //StartCoroutine(Reload());
            //}

            //if (reloading)
            //{
            //    reloadSlider.value = reloadSlider.value - Time.deltaTime;

            //    if (reloadSlider.value <= 0)
            //        reloading = false;
            //}
            #endregion Old Elemental Weapon Reload
            
            // Reload
            if (((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("X Button")) && normalGun.CheckNormalReloadable() && !reloading) || (normalGun.CheckNormalAutoReload() && !reloading))
            {
                normalGun.StartNormalReload(this);

                reloading = true;
                reloadTime = normalGun.reloadTime;
            }

            #region Old Elemental Weapon Shoot Conditions
            // Player Shoot Code
            //if (currentGun == GunTypes.Normal)
            //{
            //    NormalShoot();
            //} else if (currentGun == GunTypes.Fire)
            //{
            //    FlameShoot();
            //} else if (currentGun == GunTypes.Thunder)
            //{
            //    ElectricShoot();
            //}
            #endregion Old Elemental Weapon Shoot Conditions

            NormalShoot();

            if (reloading)
            {
                reloadTime -= Time.deltaTime;
                reloadSlider.value = reloadTime;

                if (normalGun.FinishedReloading())
                    reloading = false;
            }

            // Vortex Shoot
            if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown("Right Bumper")) && vortex.isAbilityReady())
            {
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

    private void FaceStick()
    {
        float aimAngle = Mathf.Atan2(Input.GetAxisRaw("Right JS Horizontal"), Input.GetAxisRaw("Right JS Vertical")) * Mathf.Rad2Deg;

        if (Input.GetAxisRaw("Right JS Horizontal") != 0 && Input.GetAxisRaw("Right JS Vertical") != 0)
        {
            gun.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

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

    #region Pre-Scriptable Object Reload
    //private IEnumerator Reload()
    //{
    //    reloadBarUI.SetActive(true);
    //    reloadSlider.value = reloadTime;
    //    reloading = true;

    //    ammoUI.clearAmmo();

    //    yield return new WaitForSeconds(reloadTime);

    //    reloadBarUI.SetActive(false);
    //    ammoUI.reloadAmmo();
    //    currentAmmo = setMaxAmmo;

    //    reloading = false;
    //}
    #endregion Pre-Scriptable Object Reload

    private void NormalShoot()
    {
        if ((Input.GetMouseButton(0) || Input.GetAxisRaw("Right Trigger") > 0) && normalGun.CheckClip() && !reloading  && shootTimer <= 0f)
        {
            normalGun.Shoot(gunPoint.position, gun.rotation, gunSFX);

            shootTimer = setFireRate;
        }

        if (shootTimer > 0f)
            shootTimer -= Time.deltaTime;

        #region Pre-Scriptable Object Shoot
        /*
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !reloading)
        {
            ammoUI.updateAmmo();
            currentAmmo--;

            gunSFX.Play();
            GameObject newBullet = Instantiate(bullet, gunPoint.position, gun.rotation);
            newBullet.tag = "Player Bullet";
        }*/
        #endregion Pre-Scriptable Object Shoot
    }

    #region Old Elemental Weapon Functions
    //private void FlameShoot()
    //{
    //    if (Input.GetMouseButton(0) && flamethrower.CheckClip() && !reloading)
    //    {
    //        flamethrower.Shoot(gunPoint.position, gun.rotation, gunSFX);
    //    }

    //    /*
    //    if (Input.GetMouseButton(0))
    //    {
    //        GameObject newFlame = Instantiate(flame, gunPoint.position, gun.rotation);
    //        newFlame.tag = "Player Bullet";
    //    }*/
    //}

    //private void ElectricShoot()
    //{
    //    if (Input.GetMouseButtonDown(0) && stunGun.CheckClip() && !reloading)
    //    {
    //        stunGun.Shoot(gunPoint.position, gun.rotation, gunSFX);
    //    }

    //    /*
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        GameObject newElect = Instantiate(electric, gunPoint.position, gun.rotation);
    //        newElect.tag = "Player Bullet";
    //    }*/
    //}

    //private GunTypes nextWeapon()
    //{
    //    switch (currentGun)
    //    {
    //        case GunTypes.Normal:
    //            return GunTypes.Fire;
    //        case GunTypes.Fire:
    //            return GunTypes.Thunder;
    //        case GunTypes.Thunder:
    //            return GunTypes.Normal;
    //        default:
    //            return GunTypes.Normal;
    //    }
    //}

    //private GunTypes previousWeapon()
    //{
    //    switch (currentGun)
    //    {
    //        case GunTypes.Normal:
    //            return GunTypes.Thunder;
    //        case GunTypes.Fire:
    //            return GunTypes.Normal;
    //        case GunTypes.Thunder:
    //            return GunTypes.Fire;
    //        default:
    //            return GunTypes.Normal;
    //    }
    //}

    //private Weapon getCurrentWeapon()
    //{
    //    switch (currentGun)
    //    {
    //        case GunTypes.Normal:
    //            return normalGun;
    //        case GunTypes.Fire:
    //            return flamethrower;
    //        case GunTypes.Thunder:
    //            return stunGun;
    //        default:
    //            return normalGun;
    //    }
    //}
    #endregion Old Elemental Weapon Functions

    private void initAbilityCharges()
    {
        vortex.initAbility();
    }

    private void initWeapons()
    {
        normalGun.intializeWeapon();

        #region Old Elemenetal Weapon
        //flamethrower.intializeWeapon();
        //stunGun.intializeWeapon();
        #endregion Old Elemental Weapon
    }
}
