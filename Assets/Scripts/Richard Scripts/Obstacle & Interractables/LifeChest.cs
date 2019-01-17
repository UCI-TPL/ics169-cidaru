using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of Chest
public class LifeChest : Chest {
    // Health amount to restore when retrieved
    public int hpAmount = 1;

    // Armor amount to restore when retrieved
    public int armorAmount = 1;

    // Checks to determine what the chest should give (hp, armor, both)
    public bool giveHp = true;
    public bool giveArmor = true;

    // Reward for player on collision
    public override void giveAward(GameObject player)
    {
        // Heals player's health if checked
        if (giveHp)
            player.GetComponent<PlayerHealth>().Heal(hpAmount);
    }
}
