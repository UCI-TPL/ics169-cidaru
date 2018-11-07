using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    public GameObject babyBomb;
    public GameObject timeBomb;

    public int manaCost = 20;

    private Mana manaController;

    private void Awake()
    {
        manaController = GetComponent<Mana>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Z) && manaController.getCurrentMana() > manaCost)
            {
                manaController.useMana(manaCost);

                Instantiate(babyBomb, transform.position, Quaternion.identity);
            } else if (Input.GetKeyDown(KeyCode.X) && manaController.getCurrentMana() > manaCost)
            {
                manaController.useMana(manaCost);

                Instantiate(timeBomb, transform.position, Quaternion.identity);
            }
        }
    }
}
