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

    // Current weapon being used
    public Weapon normalGun;

    // Vortex ability
    public Ability vortex;

    // Sprite of gun to flipped
    public SpriteRenderer gunSprite;

    // Area to spawn bullets
    public Transform gunPoint;

    // Fire rate of weapon
    public float setFireRate = 0.3f;

    // Initial gun position to determine proper gun point flip on certain mouse angles
    private float intialGunPointX;

    // Players sprite to flip in accordance to gun position
    private SpriteRenderer playerSprite;

    // Current position of the gun
    private Transform gun;

    // Gun Audio
    private AudioSource gunSFX;

    // Shooting timer based on fire rate
    private float shootTimer;

    // Reload UI
    private Slider reloadSlider;

    // Reloading values
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

        // Finds the player's sprite
        playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        // Grabs first child object and grabs the position (In this case the gun)
        gun = transform.GetChild(0);

        // Grabs the audio source
        gunSFX = gun.GetComponent<AudioSource>();

        // Sets initial fire delay
        shootTimer = 0f;

        // Reloading UI
        reloadSlider = GameObject.Find("Reload Bar").GetComponent<Slider>();
        reloading = false;

        // Initializes all abilities and weapons
        initAbilityCharges();
        initWeapons();

        normalGun.ActivateWeapon();
        
        intialGunPointX = gunPoint.localPosition.x;

        // Sets default control to mouse if none are set
        if (!PlayerPrefs.HasKey("Mouse"))
            PlayerPrefs.SetInt("Mouse", 1);

    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale != 0 && !GameManager.gm.cameraPanning && !GameManager.gm.spawningRooms)
        {
            // Aim in accordance to controller or mouse based on preference set
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
                // Start reload if avaliable
                normalGun.StartNormalReload(this);

                // Set and start reload values
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

            // Normal weapon shooting
            NormalShoot();

            // Function to run while reloading
            if (reloading)
            {
                // Countdowns reload and updates UI slider
                reloadTime -= Time.deltaTime;
                reloadSlider.value = reloadTime;

                // Ends reload if timer is complete
                if (normalGun.FinishedReloading())
                    reloading = false;
            }

            // Vortex Shoot
            if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown("Right Bumper")) && vortex.isAbilityReady())
            {
                vortex.PutOnCooldown();

                GameObject newVortex = Instantiate(vortex.abilityPrefab, gunPoint.position, gun.rotation);
            }
        }
	}

    // Takes mouse position and points gun towards that area
    private void FaceMouse()
    {
        // Position of the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Box in which the mouse is not tracked to prevent gun glitching out
        if (gunPoint.position.x - 0.8f >= mousePos.x || gunPoint.position.x + 0.8f <= mousePos.x ||
            gunPoint.position.y - 0.8f >= mousePos.y || gunPoint.position.y + 0.8f <= mousePos.y)
        {
            // Rotates gun to look at where the mouse is
            gun.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunPoint.position);

            // Flips gun and player in accordance to where it is pointing to
            if (gun.rotation.eulerAngles.z > 180)
            {
                gunSprite.flipY = true;
                gunPoint.localPosition = new Vector3(-intialGunPointX, gunPoint.localPosition.y);
                playerSprite.flipX = false;
            }
            else if (gun.rotation.eulerAngles.z < 180)
            {
                gunSprite.flipY = false;
                gunPoint.localPosition = new Vector3(intialGunPointX, gunPoint.localPosition.y);
                playerSprite.flipX = true;
            }
        }
    }

    private void FaceStick()
    {
        // Angle of the right joystick
        float aimAngle = Mathf.Atan2(Input.GetAxisRaw("Right JS Horizontal"), Input.GetAxisRaw("Right JS Vertical")) * Mathf.Rad2Deg;

        if (Input.GetAxisRaw("Right JS Horizontal") != 0 && Input.GetAxisRaw("Right JS Vertical") != 0)
        {
            // Rotates gun to look at where the right joystick is
            gun.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

            // Flips gun and player in accordance to where it is pointing to
            if (gun.rotation.eulerAngles.z > 180)
            {
                gunSprite.flipY = true;
                gunPoint.localPosition = new Vector3(-intialGunPointX, gunPoint.localPosition.y);
                playerSprite.flipX = false;
            }
            else if (gun.rotation.eulerAngles.z < 180)
            {
                gunSprite.flipY = false;
                gunPoint.localPosition = new Vector3(intialGunPointX, gunPoint.localPosition.y);
                playerSprite.flipX = true;
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
            // Fires gun if button is pressed and firing is avaliable
            normalGun.Shoot(gunPoint.position, gun.rotation, gunSFX);

            // Sets fire rate timer
            shootTimer = setFireRate;
        }

        // If fire rate timer is not ready, countdown it
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

    // Initializes the abilities values (vortex)
    private void initAbilityCharges()
    {
        vortex.initAbility();
    }

    // Initializes the weapons values
    private void initWeapons()
    {
        normalGun.intializeWeapon();

        #region Old Elemenetal Weapon
        //flamethrower.intializeWeapon();
        //stunGun.intializeWeapon();
        #endregion Old Elemental Weapon
    }
}
