using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCredit : MonoBehaviour
{
    public void SetFinishedCredits()
    {
        EndGameManager.endGM.activateCreditsFinished();
    }
}
