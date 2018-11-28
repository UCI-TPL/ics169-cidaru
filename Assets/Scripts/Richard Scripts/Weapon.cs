using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName = "New Weapon";
    public string weaponDescription = "Weapon Description";
    public GameObject weaponBullet;

    [Header("Weapon Properties")]
    public int setMaxClipAmmo = 10;
    public float setCooldown = 3f;

    [HideInInspector]
    public int currentClipAmmo;

    [HideInInspector]
    public int currentTotalAmmo;

    public void Reload()
    {

    }

    public bool CheckClip()
    {
        return (currentClipAmmo < setMaxClipAmmo && currentTotalAmmo > 0);
    }
}
