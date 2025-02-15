﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStatue : MonoBehaviour
{
    // Dialogue file to be read
    public TextAsset textFile;

    public GameObject uiIndicator;

    public Turret[] turrets;

    public GameObject door;

    public AudioSource doorAudio;

    private bool activated;

    private bool playerInRange = false;

    private void Awake()
    {
        activated = false;
        playerInRange = false;
        uiIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale != 0 && (Input.GetButtonDown("A Button") || Input.GetKeyDown(KeyCode.Space)) && playerInRange)
        {
            GameManager.gm.startDialogue(textFile);

            GameManager.gm.slowRoomComplete = true;

            foreach (Turret turret in turrets)
                turret.enabled = false;

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");

            foreach (GameObject enemyBullet in enemyBullets)
                Destroy(enemyBullet);

            if (!activated)
            {
                door.SetActive(false);
                doorAudio.Play();
                activated = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On player enterance, start the dialogue from the file set
        if (collision.tag == "Player")
        {
            playerInRange = true;
            uiIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
            uiIndicator.SetActive(false);
        }
    }
}
