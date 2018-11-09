using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject {
    [Header("Ability Info")]
    public string abilityName = "New Ability";
    public string abilityDescription = "Ability Description";
    public GameObject abilityPrefab;

    [Header("Ability Properties")]
    public int maxNumberOfCharges = 1;
    public float setCooldown = 3f;

    [HideInInspector]
    public float numberOfCharges = 0;

    [HideInInspector]
    public float currentCooldown = 0;

    private void Awake()
    {
        numberOfCharges = maxNumberOfCharges;
    }

    public void PutOnCooldown()
    {
        numberOfCharges--;

        CooldownController.cdInstance.StartCooldown(this);
    }

    public bool isAbilityReady()
    {
        return numberOfCharges >= 1;
    }

    public void initCharges()
    {
        numberOfCharges = maxNumberOfCharges;
    }
}
