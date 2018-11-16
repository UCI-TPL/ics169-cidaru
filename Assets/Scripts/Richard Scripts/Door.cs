using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemies;

    public List<GameObject> doorColliders;

    private bool active;

    private SpriteRenderer roomSprite;

    public void Awake()
    {
        active = false;

        foreach (GameObject dc in doorColliders)
        {
            dc.SetActive(false);
        }

        roomSprite = transform.parent.Find("Minimap Sprite").GetComponent<SpriteRenderer>();
        roomSprite.enabled = false;
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
        if (collision.tag == "Player")
        {
            if (roomSprite.enabled == false)
            {
                roomSprite.enabled = true;
            }

            GameManager.gm.updateMinimapPosition(transform.parent.position);

            if (!isCleared())
            {
                active = true;

                foreach (GameObject dc in doorColliders)
                {
                    dc.SetActive(true);
                }
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
