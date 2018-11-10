using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemies;

    public Collider2D doorCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCleared())
        {
            doorCollider.enabled = true;
        }
    }

    public bool isCleared()
    {
        foreach (GameObject enemy in enemies)
        {
            return enemy == null;
        }

        return false;
    }
}
