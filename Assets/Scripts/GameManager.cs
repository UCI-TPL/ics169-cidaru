using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text healthUI;
    public GameObject gameOverMenu;

    private Health player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Health>();
    }

    void Start () {
    }
	
	void Update () {
        healthUI.text = "Health: " + player.currentHealth;

        if (player.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }
    }
}
