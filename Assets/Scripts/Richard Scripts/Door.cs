using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public List<GameObject> enemySpawners;

    public List<GameObject> turrets;

    public List<GameObject> doorColliders;

    private bool active;
    private List<GameObject> enemies = new List<GameObject>();
    private bool cleared;

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

        cleared = false;
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

            disableTurrets();

            GameManager.gm.updateMinimapPosition(transform.parent.position);

            StartCoroutine(StartRoom());

            if (checkEnemiesAvaliable() && !cleared && !active)
                StartCoroutine(SpawnRoom());
        }
    }

    IEnumerator StartRoom()
    {
        while (GameManager.gm.cameraPanning)
            yield return null;

        foreach (GameObject turret in turrets)
        {
            turret.SetActive(true);
        }
    }

    IEnumerator SpawnRoom()
    {
        while (GameManager.gm.cameraPanning)
            yield return null;

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

    public bool checkEnemiesAvaliable()
    {
        return enemySpawners.Count != 0;
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
        cleared = true;

        foreach (GameObject dc in doorColliders)
        {
            dc.SetActive(false);
        }
    }

    public void disableTurrets()
    {
        List<GameObject> globalTurrets = new List<GameObject>();
        globalTurrets.AddRange(GameObject.FindGameObjectsWithTag("Turret"));


        foreach (GameObject globalTurret in globalTurrets)
        {
            if (!turrets.Contains(globalTurret))
                globalTurret.SetActive(false);
        }
    }
}
