using System;
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
        //Resets time for the player and their enemies
        if (!collision.gameObject.tag.Contains("Bullet"))
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

    #region Changing Times of Important Objects
    private void changePlayerTime(GameObject player, float effect)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.currentSpeed *= effect;
    }

    private void changeEnemyTime(GameObject enemy, float effect)
    {
        RangedEnemy enemyAttack = enemy.GetComponent<RangedEnemy>();
        if (enemyAttack)
            enemyAttack.setFireTimer *= effect;

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement)
            enemyMovement.currentSpeed *= effect;
    }

    private void changeBulletTime(GameObject bullet, float effect)
    {
        Bullet bulletController = bullet.GetComponent<Bullet>();
        bulletController.movementSpeed *= effect;
    }
    #endregion
}
