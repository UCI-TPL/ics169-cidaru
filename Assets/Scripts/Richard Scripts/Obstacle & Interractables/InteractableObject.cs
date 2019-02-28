using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    // Dialogue file to be read
    public TextAsset[] textFiles;

    public GameObject uiIndicator;

    private bool playerInRange = false;

    private void Awake()
    {
        playerInRange = false;
        uiIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale != 0 && (Input.GetButtonDown("A Button") || Input.GetKeyDown(KeyCode.Space)) && playerInRange)
        {
            int textIndex = Random.Range(0, textFiles.Length);
            GameManager.gm.startDialogue(textFiles[textIndex]);
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
