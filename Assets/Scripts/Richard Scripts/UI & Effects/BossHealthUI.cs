using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour {
    public Slider hpSlider;

    private Health hp;
    public GameObject winScreen;

	// Use this for initialization
	void Awake () {
        hp = GetComponent<Health>();
        hpSlider.maxValue = hp.startingHealth;
        hpSlider.value = hp.currentHealth;
        winScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        hpSlider.value = hp.currentHealth;
        //if (hp.currentHealth <= 0)
        //    win();
	}

    void win()
    {
        winScreen.SetActive(true);
    }
}
