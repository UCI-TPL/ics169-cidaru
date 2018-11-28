using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricField : MonoBehaviour {
    public float radius = 4f;
    public LayerMask enemyLayers;

    private int dmg = 1;
    private float stunTime = 1f;

    public void Awake()
    {
        if (transform.localScale.x != radius)
            transform.localScale *= radius;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius / 2, enemyLayers);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                if (!hitCollider.GetComponent<StasisController>().stunned())
                {
                    hitCollider.GetComponent<Health>().TakeDamage(dmg);

                    hitCollider.GetComponent<StasisController>().startStun(stunTime);

                    GameObject ef = Instantiate(gameObject, hitCollider.transform.position, Quaternion.identity);
                    ef.GetComponent<ElectricField>().setDamage(dmg);
                    ef.GetComponent<ElectricField>().setStunTime(stunTime);
                }
            }
        }

        Destroy(gameObject, 1.5f);
    }

    public void setDamage(int d)
    {
        dmg = d;
    }

    public void setStunTime(float st)
    {
        stunTime = st;
    }
}
