using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAreaEffector : MonoBehaviour {

    public float timeEffect = 1f;

    public float radius = 3f;

    public float setAreaTimer;

    private float areaTimer;

    private void Awake()
    {
        transform.localScale *= radius;

        areaTimer = setAreaTimer;
    }

    private void Update()
    {
        areaTimer -= Time.deltaTime;

        if (areaTimer <= 0f)
        { 
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeTime(collision.gameObject, timeEffect);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Resets time
        ChangeTime(collision.gameObject, 1/timeEffect);
    }

    private void ChangeTime(GameObject other, float effect)
    {
        if (other.tag.Equals("Player"))
            changePlayerTime(other, effect);
        else if (other.tag.Contains("Enemy") && !other.tag.Contains("Bullet") && other.GetComponent<Enemy>().speedBubbles)
            changeEnemyTime(other, effect);
        else if (other.tag.Contains("Bullet") || other.tag.Contains("Projectile"))
            changeProjectileTime(other, effect);
        else if (other.tag.Equals("Vortex"))
            changeVortexTime(other, effect);
    }

    #region Changing Times of Important Objects
    private void changePlayerTime(GameObject player, float effect)
    {
        if (timeEffect > 1f) 
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.currentSpeed *= effect;
        }
    }

    private void changeEnemyTime(GameObject enemy, float effect)
    {
        RangedEnemy enemyAttack = enemy.GetComponent<RangedEnemy>();
        if (enemyAttack)
            enemyAttack.setFireTimer *= effect;

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement)
        {
            //enemyMovement.currentSpeed *= effect;
            enemyMovement.changeBaseSpeed(effect);
        }
    }

    private void changeProjectileTime(GameObject projectile, float effect)
    {
        Projectile projectileController = projectile.GetComponent<Projectile>();
        projectileController.movementSpeed *= effect;
    }

    private void changeVortexTime(GameObject vortex, float effect)
    {
        VortexSpawner vortexController = vortex.GetComponent<VortexSpawner>();
        vortexController.movementSpeed *= effect;
    }
    #endregion
}
