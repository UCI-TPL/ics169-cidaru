using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Take this bish");
        Health otherHealth = other.GetComponent<Health>();
        otherHealth.TakeDamage(1);
    }
}