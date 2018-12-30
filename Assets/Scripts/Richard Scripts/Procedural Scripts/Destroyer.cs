using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If spawn point lands on destroyer, then destroy spawn point as room already exists there
        if (collision.tag == "Spawn Point")
            Destroy(collision.gameObject);
    }
}
