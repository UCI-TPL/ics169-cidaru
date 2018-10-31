using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float originalSpeed = 0f;
    [HideInInspector]
    public float currentSpeed;
    public bool move;

    protected GameObject player;

    private void Start()
    {
        currentSpeed = originalSpeed;
        player = GameObject.Find("Player");
        move = true;
    }

    void FixedUpdate() {
        if (move)
            Move(player.transform);
    }

    protected void Move(Transform target)
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public IEnumerator Wait(float secs)
    {
        print("Waiting for ... " + secs);
        move = false;
        yield return new WaitForSeconds(secs);
        move = true;
    }
}
