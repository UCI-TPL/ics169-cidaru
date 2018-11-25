using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChest : Chest {
    public int hpAmount = 1;
    public int armorAmount = 1;

    public bool giveHp = true;
    public bool giveArmor = true;

    private PlayerHealth hp;

    public override void Awake()
    {
        base.Awake();

        hp = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void giveAward(GameObject player)
    {
        if (giveHp)
            hp.AddArmor(hpAmount);

        if (giveArmor)
            hp.Heal(armorAmount);
    }
}
