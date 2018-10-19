using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    /// Tracks the health of the object
    /// Based on:
    /// https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health

    public int startingHealth = 10;
    public int currentHealth;

    private bool isDead; //TODO: Make private

    private void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void HealBy(int amount)
    {
        if (currentHealth + amount >= startingHealth)
            currentHealth = startingHealth;
        else
            currentHealth += amount;
    }

    private void Death()
    {
        isDead = true;
    }

    public bool dead()
    {
        return isDead;
    }

    public void Reset()
    {
        currentHealth = startingHealth;
        isDead = false;
    }
}
