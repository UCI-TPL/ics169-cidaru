using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAreaEffector : MonoBehaviour {

    public float timeEffect = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeTime(collision.gameObject, timeEffect);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeTime(collision.gameObject, 1/timeEffect);
    }

    private void ChangeTime(GameObject other, float effect)
    {
        if (other.tag.Equals("Player"))
            changePlayerTime(other, effect);
        else if (other.tag.Equals("Enemy"))
            changeEnemyTime(other, effect);
        else if (other.tag.Contains("Bullet"))
            changeBulletTime(other, effect);
    }

    private void changePlayerTime(GameObject player, float effect)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.currentSpeed *= effect;
    }

    private void changeEnemyTime(GameObject enemy, float effect)
    {
        //TODO: change enemy time
    }

    private void changeBulletTime(GameObject bullet, float effect)
    {
        Bullet bulletController = bullet.GetComponent<Bullet>();
        bulletController.movementSpeed *= effect;
    }
}
