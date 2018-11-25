using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private bool opened;

    public virtual void Awake()
    {
        opened = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            opened = true;
            giveAward(collision.gameObject);
        }
    }

    public virtual void giveAward(GameObject player)
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
            player.GetComponent<BombController>().babyBomb.increaseMaxCharge();
        else if (rand == 1)
            player.GetComponent<BombController>().hasteBomb.increaseMaxCharge();
        else if (rand == 2)
            player.GetComponent<BombController>().slowBomb.increaseMaxCharge();
        else if (rand == 3)
            player.GetComponent<GunController>().vortex.increaseMaxCharge();
    }
}
