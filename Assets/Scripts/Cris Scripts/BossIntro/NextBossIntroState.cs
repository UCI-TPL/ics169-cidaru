using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBossIntroState : MonoBehaviour
{
    public void ChangeToNextState()
    {
        BossIntroGameManager.bossIntroGM.NextState();
    }
}
