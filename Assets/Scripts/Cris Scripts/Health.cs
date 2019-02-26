using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int startingHealth = 1;

    [Header("Sound Effect Bank (For Enemies)")]
    public SFXStorage audioClips;
    
    [HideInInspector]
    public int currentHealth;

    protected bool isDead; //TODO: Make private
    protected bool invincible = false;

    private AudioClip chosenSFX;
    private AudioSource audioSource;

    public virtual void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;

        if (gameObject.tag == "Enemy")
        {
            chosenSFX = audioClips.soundEffs[Random.Range(0, audioClips.soundEffs.Count)];
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = chosenSFX;
        }
    }

    public virtual void TakeDamage(int amount)
    {
        if (!invincible)
        {
            currentHealth -= amount;

            if (gameObject.tag == "Enemy")
            {
                audioSource.pitch = Random.Range(0.8f, 1f);
                audioSource.Play();
            }
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

    public bool getInvincible()
    {
        return invincible;
    }

}
