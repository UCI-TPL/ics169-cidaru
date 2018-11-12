using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    

    public int startingHealth = 1;
    
    [HideInInspector]
    public int currentHealth;

    protected bool isDead; //TODO: Make private
    protected bool invincible = false;

    public virtual void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    public virtual void TakeDamage(int amount)
    {
        if (!invincible)
        {
            currentHealth -= amount;
        }

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

    public void setInvincible(bool invin)
    {
        invincible = invin;
    }

}
