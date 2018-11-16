using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {

    private Image[] ammoImages;
    private int ammoIndexToDisable;

    // Use this for initialization
    void Awake () {
        ammoImages = transform.GetComponentsInChildren<Image>();
        ammoIndexToDisable = ammoImages.Length - 1;
    }

    public void updateAmmo()
    {
        if (ammoIndexToDisable < 0)
        {
            return;
        }

        ammoImages[ammoIndexToDisable].enabled = false;
        ammoIndexToDisable--;
    }

    public void reloadAmmo()
    {
        foreach (Image ammoImage in ammoImages)
        {
            ammoImage.enabled = true;
        }

        ammoIndexToDisable = ammoImages.Length - 1;
    }

    public void clearAmmo()
    {
        foreach (Image ammoImage in ammoImages)
        {
            ammoImage.enabled = false;
        }
    }
}
