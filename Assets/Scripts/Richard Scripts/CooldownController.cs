using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownController : MonoBehaviour {

    public static CooldownController cdInstance;

    private List<Ability> abilitiesOnCooldown = new List<Ability>();

    private void Awake()
    {
        if (cdInstance == null)
        {
            cdInstance = this;
        } else if (cdInstance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this); // CHANGE LATER
    }

    private void Update()
    {
        foreach (Ability ability in abilitiesOnCooldown)
        {
            ability.currentCooldown -= Time.deltaTime;
            
            if (ability.currentCooldown <= 0f)
            {
                ability.numberOfCharges++;

                if (ability.numberOfCharges == ability.maxNumberOfCharges)
                {
                    abilitiesOnCooldown.Remove(ability);
                } else
                {
                    ability.currentCooldown = ability.setCooldown;
                }
            }
        }
    }

    public void StartCooldown(Ability ability)
    {
        if (!abilitiesOnCooldown.Contains(ability))
        {
            ability.currentCooldown = ability.setCooldown;

            abilitiesOnCooldown.Add(ability);
        }
    }
}
