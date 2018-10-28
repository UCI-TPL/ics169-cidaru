using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float originalSpeed = 0f;
    //[HideInInspector]
    public float currentSpeed;

    protected GameObject player;

    private void Start()
    {
        currentSpeed = originalSpeed;
        player = GameObject.Find("Player");
    }

    void FixedUpdate() {
        Move(player.transform);
    }

    protected void Move(Transform target)
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
