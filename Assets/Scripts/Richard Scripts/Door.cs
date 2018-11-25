using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemySpawners;

    public List<GameObject> doorColliders;

    private bool active;
    private List<GameObject> enemies;

    private SpriteRenderer roomSprite;

    public void Awake()
    {
        active = false;

        foreach (GameObject enemy in enemySpawners)
        {
            enemy.SetActive(false);
        }

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

            if (!isCleared() && !active)
            {
                active = true;

                foreach (GameObject enemy in enemySpawners)
                {
                    enemy.SetActive(true);
                }

                enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

                foreach (GameObject dc in doorColliders)
                {
                    dc.SetActive(true);
                }
            }
        }
    }

    public bool isCleared()
    {
        foreach (GameObject enemySpawner in enemySpawners)
        {
            return enemySpawner == null;
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
