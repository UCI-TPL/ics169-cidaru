using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemies;

    public List<GameObject> doorColliders;

    private bool active;

    public void Awake()
    {
        active = false;

        foreach (GameObject dc in doorColliders)
        {
            dc.SetActive(false);
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

            foreach (GameObject dc in doorColliders)
            {
                dc.SetActive(true);
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

        foreach (GameObject dc in doorColliders)
        {
            dc.SetActive(false);
        }
    }
}
