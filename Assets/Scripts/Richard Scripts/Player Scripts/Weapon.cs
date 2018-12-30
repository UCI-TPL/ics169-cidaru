using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scriptable object for "Weapon"
// Used to store data on what a weapon does

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    // Info on the weapon such as name, description, and bullet prefab
    [Header("Weapon Info")]
    public string weaponName = "New Weapon";
    public string weaponDescription = "Weapon Description";
    public GameObject weaponBullet;

    // Properties of weapon such as max clip ammo, reload time, UI, and sound effect
    [Header("Weapon Properties")]
    public int setMaxClipAmmo = 10;
    public float reloadTime = 2;
    public string reloadUIName = "Reload Bar";
    public bool usesBullets = true;
    public string ammoUIName;
    public AudioClip sfx;

    // Current ammo clip of weapon
    [HideInInspector]
    public int currentClipAmmo;

    // Current total ammo of weapon
    [HideInInspector]
    public int currentTotalAmmo;

    // Ammo and Reload UI
    private Slider reloadSlider;
    private GameObject ammoUIObject;
    private Slider ammoSlider;
    private AmmoUI ammoUI;

    // Activates the UI of the weapon if active
    public void ActivateWeapon()
    {
        ammoUIObject.SetActive(true);
    }

    // Deactivates the UI of the weapon if not active
    public void DeactiveWeapon()
    {
        ammoUIObject.SetActive(false);
    }

    // Initializes the values for the weapon
    public void intializeWeapon()
    {
        // Sets the current clip to be at max
        currentClipAmmo = setMaxClipAmmo; // temp

        // Sets the current total ammo to be at max
        currentTotalAmmo = setMaxClipAmmo * 100;

        // Finds the respective Reload UI
        reloadSlider = GameObject.Find(reloadUIName).GetComponent<Slider>();

        // Finds the respective Ammo UI
        ammoUIObject = GameObject.Find(ammoUIName);

        // If ammo uses a bullet or charge base, sets up the appropriate UI
        if (usesBullets)
        {
            ammoUI = ammoUIObject.GetComponent<AmmoUI>();
        }
        else
        {
            // Sets slider and its values
            ammoSlider = ammoUIObject.GetComponent<Slider>();
            ammoSlider.maxValue = setMaxClipAmmo;
            ammoSlider.value = setMaxClipAmmo; // temp
        }

        // Deactivate UI for weapon
        reloadSlider.gameObject.SetActive(false);
        ammoUIObject.SetActive(false);
    }

    // Check if the current clip is greater than 0
    public bool CheckClip()
    {
        return currentClipAmmo > 0;
    }

    // Function to fire the "bullet" of the weapon
    public void Shoot(Vector3 pos, Quaternion rot, AudioSource aController)
    {
        // Updates the UI based on if it is bullet base or not
        if (usesBullets)
            ammoUI.updateAmmo();
        else
            ammoSlider.value--;

        // Decreases the current clip ammo
        currentClipAmmo--;

        // Sets the weapon sound effect
        aController.clip = sfx;

        // Plays the weapon sound effect
        aController.Play();

        // Spawns and sets the new tag of the "bullet" spawned
        GameObject newBullet = Instantiate(weaponBullet, pos, rot);
        newBullet.tag = "Player Bullet";
    }

    #region Normal Bullet Functions
    // Starts the coroutine of the reload for the normal weapon
    public void StartNormalReload(MonoBehaviour controller)
    {
        controller.StartCoroutine(NormalReload());
    }

    // Coroutine of the normal reload
    IEnumerator NormalReload()
    {
        // Clears the ammo UI on screen
        ammoUI.clearAmmo();

        // Activates the reload UI
        reloadSlider.gameObject.SetActive(true);

        // Deactivates the ammo UI
        ammoUIObject.SetActive(false);

        // Sets the values for the reload UI
        reloadSlider.maxValue = reloadTime;
        reloadSlider.value = reloadTime;

        // Waits reload time specified
        yield return new WaitForSeconds(reloadTime);
        
        // Reloads the clip
        currentClipAmmo = setMaxClipAmmo;

        // Deactivates the reload UI
        reloadSlider.gameObject.SetActive(false);

        // Activates the ammo UI
        ammoUIObject.SetActive(true);
        
        // Resets the ammo UI on screen
        ammoUI.reloadAmmo();
    }

    // Checks if the normal weapon can be reloaded
    public bool CheckNormalReloadable()
    {
        return currentClipAmmo < setMaxClipAmmo;
    }

    // Checks if the normal weapon is in auto reload range (clip is at 0)
    public bool CheckNormalAutoReload()
    {
        return currentClipAmmo == 0;
    }

    // Checks if the clip has been finished reloading
    public bool FinishedReloading()
    {
        return currentClipAmmo == setMaxClipAmmo;
    }
    #endregion Normal Bullet Functions

    #region Elemental Bullet Functions
    public void StartReload(MonoBehaviour controller)
    {
        controller.StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        if (usesBullets)
            ammoUI.clearAmmo();

        reloadSlider.gameObject.SetActive(true);
        ammoUIObject.SetActive(false);

        reloadSlider.maxValue = reloadTime;
        reloadSlider.value = reloadTime;

        yield return new WaitForSeconds(reloadTime);

        currentTotalAmmo += currentClipAmmo;

        if (currentTotalAmmo < setMaxClipAmmo)
        {
            currentClipAmmo = currentTotalAmmo;
            currentTotalAmmo = 0;
        }
        else
        {
            currentClipAmmo = setMaxClipAmmo;
            currentTotalAmmo -= setMaxClipAmmo;
        }

        reloadSlider.gameObject.SetActive(false);
        ammoUIObject.SetActive(true);

        if (usesBullets)
            ammoUI.reloadAmmo(); // NEEDS TO RELOAD TO PROPER NUMBER TOO
        else
            ammoSlider.value = ammoSlider.maxValue;
    }

    public bool CheckReloadable()
    {
        return (currentClipAmmo < setMaxClipAmmo && currentTotalAmmo > 0);
    }

    public bool CheckAutoReload()
    {
        return currentClipAmmo == 0 && currentTotalAmmo > 0;
    }

    public void AddAmmo(int ammo)
    {
        currentTotalAmmo += ammo;
    }
    #endregion Elemental Bullet Functions
}
