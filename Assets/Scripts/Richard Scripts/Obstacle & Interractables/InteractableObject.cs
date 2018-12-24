using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    // Dialogue file to be read
    public TextAsset textFile;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On player enterance, start the dialogue from the file set
        if (collision.tag == "Player")
        {
            GameManager.gm.startDialogue(textFile);
        }
    }
}
