using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scriptable object for "Ability"
// Used to store data on what an ability does

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject {

    // Info on the ability such as name, description, prefab, and UI
    [Header("Ability Info")]
    public string abilityName = "New Ability";
    public string abilityDescription = "Ability Description";
    public GameObject abilityPrefab;
    public string cooldownUIName;
    //public string chargeUIName;

    // Properties of ability such as max charges and cooldown
    [Header("Ability Properties")]
    public int setMaxNumberOfCharges = 1;
    public float setCooldown = 3f;

    // Current number of charges for ability
    [HideInInspector]
    public int numberOfCharges = 0;

    // Current number of max charges for ability
    [HideInInspector]
    public int maxNumberOfCharges;

    // Current cooldown of ability
    [HideInInspector]
    public float currentCooldown = 0;

    // Slider UI for ability to indicate cooldown time
    [HideInInspector]
    private Slider cooldownUI;

    //[HideInInspector]
    //private Text chargeUI;

    // DOES NOT ACTUALLY DO THE SETTING
    private void Awake()
    {
        maxNumberOfCharges = setMaxNumberOfCharges;
        numberOfCharges = maxNumberOfCharges;
    }

    // Function to call to start ability cooldown
    public void PutOnCooldown()
    {
        // Decreases current charges
        numberOfCharges--;

        //chargeUI.text = "" + numberOfCharges;

        // Starts cooldown
        CooldownController.cdInstance.StartCooldown(this);
    }

    // Checks if ability is read (has 1 or more charges)
    public bool isAbilityReady()
    {
        return numberOfCharges >= 1;
    }

    // Increases the current number of charges by one
    public void increaseCharge()
    {
        numberOfCharges++;

        //chargeUI.text = "" + numberOfCharges;
    }

    // Count downs the cooldown of ability
    public void updateCooldown()
    {
        // Decreases the current cooldown based on time
        currentCooldown -= Time.deltaTime;
        
        // Resets slider UI to max if cooldown is over
        if (setCooldown - currentCooldown > setCooldown)
        {
            cooldownUI.value = setCooldown;
            return;
        }

        // Sets slider UI to appropriate value of cooldown
        cooldownUI.value = setCooldown - currentCooldown;
    }

    // Start cooldown if max charges increased
    private void increaseChargeCD()
    {
        //chargeUI.text = "" + numberOfCharges;

        CooldownController.cdInstance.StartCooldown(this);
    }

    // Increases max number of charges of ability
    public void increaseMaxCharge()
    {
        maxNumberOfCharges++;

        // Starts charging cooldown for the new charge
        increaseChargeCD();
    }

    // Call function to initialize the original ability values
    public void initAbility()
    {
        // Sets the number of charges originally set
        maxNumberOfCharges = setMaxNumberOfCharges;
        numberOfCharges = maxNumberOfCharges;

        // Sets the cooldown
        currentCooldown = setCooldown;

        //chargeUI = GameObject.Find(chargeUIName).GetComponent<Text>();
        //chargeUI.text = "" + numberOfCharges;

        // Find approiate cooldown UI for ability and sets values accordingly
        cooldownUI = GameObject.Find(cooldownUIName).GetComponent<Slider>();
        cooldownUI.maxValue = setCooldown;
        cooldownUI.value = setCooldown;
    }
}
