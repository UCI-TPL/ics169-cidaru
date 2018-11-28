using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private bool opened;

    private bool nextTo;
    private GameObject player;

    public virtual void Awake()
    {
        opened = false;
        nextTo = false;
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        if (nextTo && Input.GetKeyDown(KeyCode.E))
        {
            giveAward(player);
            opened = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !opened)
            nextTo = true;
        else
            nextTo = false;
    }

    public virtual void giveAward(GameObject player)
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            player.GetComponent<BombController>().babyBomb.increaseMaxCharge();
        else if (rand == 1)
            player.GetComponent<BombController>().slowBomb.increaseMaxCharge();
        else if (rand == 2)
            player.GetComponent<GunController>().vortex.increaseMaxCharge();
    }
}
