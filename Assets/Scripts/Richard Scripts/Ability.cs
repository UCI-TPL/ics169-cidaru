using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    [Header("Ability Info")]
    public string abilityName = "New Ability";
    public string abilityDescription = "Ability Description";
    public GameObject abilityPrefab;
    public string cooldownUIName;
    public string chargeUIName;

    [Header("Ability Properties")]
    public int maxNumberOfCharges = 1;
    public float setCooldown = 3f;

    [HideInInspector]
    public float numberOfCharges = 0;

    [HideInInspector]
    public float currentCooldown = 0;

    [HideInInspector]
    private Slider cooldownUI;

    [HideInInspector]
    private Text chargeUI;

    private void Awake()
    {
        numberOfCharges = maxNumberOfCharges;
    }

    public void PutOnCooldown()
    {
        numberOfCharges--;

        chargeUI.text = "" + numberOfCharges;

        CooldownController.cdInstance.StartCooldown(this);
    }

    public bool isAbilityReady()
    {
        return numberOfCharges >= 1;
    }

    public void increaseCharge()
    {
        numberOfCharges++;

        chargeUI.text = "" + numberOfCharges;
    }

    public void updateCooldown()
    {
        currentCooldown -= Time.deltaTime;
        
        if (setCooldown - currentCooldown > setCooldown)
        {
            cooldownUI.value = setCooldown;
            return;
        }
        cooldownUI.value = setCooldown - currentCooldown;
    }

    public void initAbility()
    {
        numberOfCharges = maxNumberOfCharges;

        currentCooldown = setCooldown;

        chargeUI = GameObject.Find(chargeUIName).GetComponent<Text>();
        chargeUI.text = "" + numberOfCharges;

        cooldownUI = GameObject.Find(cooldownUIName).GetComponent<Slider>();
        cooldownUI.maxValue = setCooldown;
        cooldownUI.value = setCooldown;
    }
}
