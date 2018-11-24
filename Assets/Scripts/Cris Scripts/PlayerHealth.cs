using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
    /* Tracks player health. Includes player-specific effects
    Based on:
    https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
    */

    public Slider healthBar;
    public Slider armorBar;

    public int armor;

    private PlayerController player;

    public override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        healthBar.maxValue = startingHealth;
        healthBar.value = startingHealth;

        invincible = false;
    }

    public override void TakeDamage(int amount)
    {
        if (!invincible)
        {
            if (armor < amount)
            {
                amount -= armor;
                armor = 0;
            } else
            {
                armor -= amount;
                amount = 0;
            }

            currentHealth -= amount;
            invincible = true;
            player.startInvincibility();
        }

        updateHealthBar();
        updateArmorBar();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth + amount >= startingHealth)
            currentHealth = startingHealth;
        else
            currentHealth += amount;

        updateHealthBar();
    }

    public void AddArmor(int amount)
    {
        armor += amount;

        updateArmorBar();
    }

    public void Reset()
    {
        currentHealth = startingHealth;
        //updateHealthBar();
        isDead = false;
    }
    
    private void updateHealthBar()
    {
        healthBar.value = currentHealth;
        //Debug.Log("updated health bar");
    }

    private void updateArmorBar()
    {
        armorBar.value = armor;
    }

    public bool isInvincible()
    {
        return invincible;
    }

    public void setVulnerable()
    {
        invincible = false;
    }
}
