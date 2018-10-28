using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float originalSpeed = 0f;
    //[HideInInspector]
    public float currentSpeed;

    private GameObject player;

    private void Start()
    {
        currentSpeed = originalSpeed;
        player = GameObject.Find("Player");
    }

    void Update() {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
}
