using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemies;

    public List<Collider2D> doorColliders;

    private bool active;

    public void Awake()
    {
        active = false;

        foreach (Collider2D dc in doorColliders)
        {
            dc.enabled = false;
        }
    }

    private void Update()
    {
        if (active)
        {
            checkRoomCleared();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCleared())
        {
            active = true;

            foreach (Collider2D dc in doorColliders)
            {
                dc.enabled = true;
            }
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

    public void checkRoomCleared()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        active = false;

        foreach (Collider2D dc in doorColliders)
        {
            dc.enabled = false;
        }
    }
}
