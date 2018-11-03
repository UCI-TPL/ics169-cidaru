using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour {
    protected GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public abstract void Attack();
}
