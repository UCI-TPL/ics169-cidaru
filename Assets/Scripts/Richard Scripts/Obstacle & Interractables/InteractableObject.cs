using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    // Dialogue file to be read
    public TextAsset[] textFiles;

    private bool triggered = false;

    private void Awake()
    {
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On player enterance, start the dialogue from the file set
        if (collision.tag == "Player" && !triggered)
        {
            int textIndex = Random.Range(0, textFiles.Length);
            GameManager.gm.startTutorialDialogue(textFiles[textIndex]);
            triggered = true;
        }
    }
}
