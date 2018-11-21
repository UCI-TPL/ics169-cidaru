using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public TextAsset textFile;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !GameManager.gm.getDialogue())
        {
            GameManager.gm.startDialogue(textFile);
        }
    }
}
