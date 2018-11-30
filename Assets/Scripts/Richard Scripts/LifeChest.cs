using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChest : Chest {
    public int hpAmount = 1;
    public int armorAmount = 1;

    public bool giveHp = true;
    public bool giveArmor = true;

    public override void giveAward(GameObject player)
    {
        if (giveHp)
            player.GetComponent<PlayerHealth>().Heal(hpAmount);

        if (giveArmor)
            player.GetComponent<PlayerHealth>().AddArmor(armorAmount);
    }
}
