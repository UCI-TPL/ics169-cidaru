using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health {
    /* Tracks player health. Includes player-specific effects
    Based on:
    https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
    */

    [HideInInspector]
    public Slider healthBar;

    private PlayerController player;
    private CameraShake shake;

    public override void Awake()
    {
        base.Awake();

        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        player = GetComponent<PlayerController>();
        shake = GetComponent<CameraShake>();
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
            currentHealth -= amount;
            invincible = true;
            player.startInvincibility();
        }

        shake.startShake();

        updateHealthBar();

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

    public void MaxHeal()
    {
        currentHealth = startingHealth;

        if (isDead)
            isDead = false;

        updateHealthBar();
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
