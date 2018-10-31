using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text healthUI;
    public Text manaUI;

    public GameObject gameOverMenu;

    private Health playerHp;
    private Mana playerMana;

    private void Awake()
    {
        playerHp = GameObject.Find("Player").GetComponent<Health>();
        playerMana = GameObject.Find("Player").GetComponent<Mana>();
    }

    void Start () {
    }
	
	void Update () {
        healthUI.text = "Health: " + playerHp.currentHealth;
        manaUI.text = "Mana: " + playerMana.getCurrentMana();

        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }
    }
}
