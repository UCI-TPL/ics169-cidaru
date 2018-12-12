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
    public Slider shieldBar;

    public int maxShield = 5;

    public int shield;

    private PlayerController player;
    private CameraShake shake;

    public override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerController>();
        shake = GetComponent<CameraShake>();
    }

    private void Start()
    {
        healthBar.maxValue = startingHealth;
        healthBar.value = startingHealth;

        shieldBar.maxValue = maxShield;
        shieldBar.value = 0;
        shield = 0;

        invincible = false;
    }

    public override void TakeDamage(int amount)
    {
        if (!invincible)
        {
            if (shield < amount)
            {
                amount -= shield;
                shield = 0;
            } else
            {
                shield -= amount;
                amount = 0;
            }

            currentHealth -= amount;
            invincible = true;
            player.startInvincibility();
        }

        shake.startShake();

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
        if (shield + amount >= maxShield)
            shield = maxShield;
        else
            shield += amount;

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
        shieldBar.value = shield;
    }

    public bool isMaxHealth()
    {
        return currentHealth == startingHealth;
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
