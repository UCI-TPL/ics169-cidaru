using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text healthUI;
    public Text manaUI;
    public Text ammoUI;

    public GameObject gameOverMenu;

    private GameObject player;
    private Health playerHp;
    private Mana playerMana;
    private GunController gun;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();
        playerMana = player.GetComponent<Mana>();
        gun = player.GetComponent<GunController>();
    }

    void Start () {
    }
	
	void Update () {
        healthUI.text = "Health: " + playerHp.currentHealth;
        manaUI.text = "Mana: " + playerMana.getCurrentMana();
        ammoUI.text = "Ammo: " + gun.getCurrentAmmoState();

        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }
    }
}
