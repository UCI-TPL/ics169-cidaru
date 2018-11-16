using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomTriggers : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.gm.updateMinimapPosition(transform.parent.position);
        }
    }
}
