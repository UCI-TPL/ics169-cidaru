using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            giveAward(collision.gameObject);

            Destroy(gameObject);
        }
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
