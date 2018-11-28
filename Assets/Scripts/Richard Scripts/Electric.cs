using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Projectile {
    public GameObject electricField;
    public float stunTimer = 2f;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Enemy" || col.tag == "Enemy Boss") && tag == "Player Bullet")
        {
            col.GetComponent<Health>().TakeDamage(dmg);
            col.GetComponent<StasisController>().startStun(stunTimer);

            GameObject ef = Instantiate(electricField, col.transform.position, Quaternion.identity);
            ef.GetComponent<ElectricField>().setDamage(dmg);
            ef.GetComponent<ElectricField>().setStunTime(stunTimer);
            Destroy(gameObject);
        }
        else if ((col.tag == "Player" || col.tag == "Enemy") && tag == "Vortex Projectile")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            if (col.tag == "Enemy")
                col.GetComponent<StasisController>().startStun(stunTimer);

            GameObject ef = Instantiate(electricField, col.transform.position, Quaternion.identity);
            ef.transform.GetComponent<ElectricField>().setDamage(dmg);
            ef.transform.GetComponent<ElectricField>().setStunTime(stunTimer);
            Destroy(gameObject);
        }
        else if (col.tag == "Obstacle" && tag != "Rotating Bullet")
        {
            GameObject ef = Instantiate(electricField, transform.position, Quaternion.identity);
            ef.GetComponent<ElectricField>().setDamage(dmg);
            ef.GetComponent<ElectricField>().setStunTime(stunTimer);
            Destroy(gameObject);
        }
        else if (col.tag == "Destroyable")
        {
            col.GetComponent<Health>().TakeDamage(dmg);

            GameObject ef = Instantiate(electricField, col.transform.position, Quaternion.identity);
            ef.GetComponent<ElectricField>().setDamage(dmg);
            ef.GetComponent<ElectricField>().setStunTime(stunTimer);
            Destroy(gameObject);
        }
    }
}
