using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAttractor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hp Pot" && !transform.parent.GetComponent<PlayerHealth>().isMaxHealth())
            collision.GetComponent<HealthDrop>().StartAttraction(gameObject.transform.parent);
    }
}
