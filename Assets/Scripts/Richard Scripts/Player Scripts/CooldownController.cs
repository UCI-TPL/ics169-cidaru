using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownController : MonoBehaviour {

    // Static variable of the controller
    public static CooldownController cdInstance;

    // List of abilities that are currently on cooldown
    private List<Ability> abilitiesOnCooldown = new List<Ability>();

    private void Awake()
    {
        // Sets this controller to be current cooldown controller
        // Destroys this controller if another cooldown controller already exist
        if (cdInstance == null)
            cdInstance = this;
        else if (cdInstance != this)
            Destroy(this);
    }

    private void Update()
    {
        // Loops through each abiltiy and calculates cooldown
        foreach (Ability ability in abilitiesOnCooldown)
        {
            // Decreases cooldown and updates UI for ability
            ability.updateCooldown();
            
            // If cooldown is over, add a charge
            if (ability.currentCooldown <= 0f)
            {
                ability.increaseCharge();

                // If max charges remove from list
                // Else reset cooldown
                if (ability.numberOfCharges == ability.maxNumberOfCharges)
                    abilitiesOnCooldown.Remove(ability);
                else
                    ability.currentCooldown = ability.setCooldown;
            }
        }
    }

    // Resets ability cooldown and adds to cooldown list
    // params: ability, Ability to be set on cooldown
    public void StartCooldown(Ability ability)
    {
        if (!abilitiesOnCooldown.Contains(ability))
        {
            ability.currentCooldown = ability.setCooldown;

            abilitiesOnCooldown.Add(ability);
        }
    }
}
