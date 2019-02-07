using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSetup : MonoBehaviour {

    public Toggle controls;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("Mouse"))
        {
            PlayerPrefs.SetInt("Mouse", 1);
        }

        if (PlayerPrefs.GetInt("Mouse") != 0)
            controls.isOn = false;
        else
            controls.isOn = true;
	}
	
	public void SavePref()
    {
        if (controls.isOn)
            PlayerPrefs.SetInt("Mouse", 0);
        else
            PlayerPrefs.SetInt("Mouse", 1);
    }
}
