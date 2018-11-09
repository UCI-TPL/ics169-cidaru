using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public enum Opening
    {
        Bottom,
        Top,
        Left,
        Right
    }

    public Text healthUI;
    //public Text manaUI;
    public Text ammoUI;

    public GameObject gameOverMenu;

    public Texture2D cursor;

    private GameObject player;
    private Health playerHp;
    //private Mana playerMana;
    private GunController gun;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();
        // playerMana = player.GetComponent<Mana>();
        gun = player.GetComponent<GunController>();

        Cursor.SetCursor(cursor, new Vector2(512 / 2, 512 / 2), CursorMode.Auto);
    }

    void Start () {
    }
	
	void Update () {
        healthUI.text = "Health: " + playerHp.currentHealth;
        // manaUI.text = "Mana: " + playerMana.getCurrentMana();
        ammoUI.text = "Ammo: " + gun.getCurrentAmmoState();

        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }
    }
}
