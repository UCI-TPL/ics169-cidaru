using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    

    public int startingHealth = 1;
    
    [HideInInspector]
    public int currentHealth;

    protected bool isDead; //TODO: Make private

    public virtual void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public bool dead()
    {
        return isDead;
    }

    protected void Death()
    {
        isDead = true;
    }
    
}
