using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeleeEnemy))]
public class WhirlwindMovement : RangedMovement {
    //Movement for whirlwind-specific enemies

    private MeleeEnemy whirlwindAttack;

    private void Start()
    {
        base.setStartVars();
        whirlwindAttack = GetComponent<MeleeEnemy>();
    }

    public override void Move(bool aggressing)
    {
        if (whirlwindAttack.spinning)
            base.Move(aggressing); //Move like Ranged Enemy
        else
            MoveTo(player.transform.position); //Move like normal
    }
}
