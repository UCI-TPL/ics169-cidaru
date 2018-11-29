using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public TextAsset textFile;

    private bool nextTo;

    public void Awake()
    {
        nextTo = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !GameManager.gm.getDialogue() && nextTo)
        {
            GameManager.gm.startDialogue(textFile);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nextTo = true;
        } else
        {
            nextTo = false;
        }
    }
}
