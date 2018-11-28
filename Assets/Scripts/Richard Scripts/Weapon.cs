using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName = "New Weapon";
    public string weaponDescription = "Weapon Description";
    public GameObject weaponBullet;

    [Header("Weapon Properties")]
    public int setMaxClipAmmo = 10;
    public float reloadTime = 2;
    public string reloadUIName = "Reload Bar";
    public bool usesBullets = true;
    public string ammoUIName;
    public AudioClip sfx;

    [HideInInspector]
    public int currentClipAmmo;

    [HideInInspector]
    public int currentTotalAmmo;

    private Slider reloadSlider;
    private Coroutine reloadFunction;
    private GameObject ammoUIObject;
    private Slider ammoSlider;
    private AmmoUI ammoUI;

    public void Reload(MonoBehaviour controller)
    {
        reloadFunction = controller.StartCoroutine(StartReload());
    }

    private IEnumerator StartReload()
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

    // FOR NORMAL GUN
    public void InfiniteReload()
    {
        currentClipAmmo = setMaxClipAmmo;
    }

    public bool CheckReloadable()
    {
        return (currentClipAmmo < setMaxClipAmmo && currentTotalAmmo > 0);
    }

    public bool CheckAutoReload()
    {
        return currentClipAmmo == 0 && currentTotalAmmo > 0;
    }

    public bool CheckClip()
    {
        return currentClipAmmo > 0;
    }

    public void Shoot(Vector3 pos, Quaternion rot, AudioSource aController)
    {
        if (usesBullets)
            ammoUI.updateAmmo();
        else
            ammoSlider.value--;

        currentClipAmmo--;

        aController.clip = sfx;
        
        aController.Play();

        GameObject newBullet = Instantiate(weaponBullet, pos, rot);
        newBullet.tag = "Player Bullet";
    }

    public void AddAmmo(int ammo)
    {
        currentTotalAmmo += ammo;
    }

    public void ActivateWeapon()
    {
        ammoUIObject.SetActive(true);
    }

    public void DeactiveWeapon()
    {
        ammoUIObject.SetActive(false);
    }

    public void intializeWeapon()
    {
        currentClipAmmo = setMaxClipAmmo; // temp
        currentTotalAmmo = setMaxClipAmmo * 100;

        reloadSlider = GameObject.Find(reloadUIName).GetComponent<Slider>();

        ammoUIObject = GameObject.Find(ammoUIName);

        if (usesBullets)
        {
            ammoUI = ammoUIObject.GetComponent<AmmoUI>();
        }
        else
        {
            ammoSlider = ammoUIObject.GetComponent<Slider>();
            ammoSlider.maxValue = setMaxClipAmmo;
            ammoSlider.value = setMaxClipAmmo; // temp
        }

        ammoUIObject.SetActive(false);
    }
}
