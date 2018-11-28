using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemySpawners;

    public List<GameObject> turrets;

    public List<GameObject> doorColliders;

    private bool active;
    private List<GameObject> enemies = new List<GameObject>();

    private SpriteRenderer roomSprite;

    public void Awake()
    {
        active = false;

        foreach (GameObject enemy in enemySpawners)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject turret in turrets)
        {
            turret.SetActive(false);
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

            checkTurrets();

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

    public void checkTurrets()
    {
        List<GameObject> globalTurrets = new List<GameObject>();
        globalTurrets.AddRange(GameObject.FindGameObjectsWithTag("Turret"));
        

        foreach (GameObject globalTurret in globalTurrets)
        {
            if (!turrets.Contains(globalTurret))
                globalTurret.SetActive(false);
        }

        foreach (GameObject turret in turrets)
        {
            turret.SetActive(true);
        }
    }
}
