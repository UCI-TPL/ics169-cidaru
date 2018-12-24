using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomTriggers : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            disableTurret();
            GameManager.gm.updateMinimapPosition(transform.parent.position);
        }
    }

    public void disableTurret()
    {
        List<GameObject> globalTurrets = new List<GameObject>();
        globalTurrets.AddRange(GameObject.FindGameObjectsWithTag("Turret"));


        foreach (GameObject globalTurret in globalTurrets)
        {
            globalTurret.SetActive(false);
        }
    }
}
