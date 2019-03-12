using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextEndState : MonoBehaviour
{
    public void ChangeToNextState()
    {
        EndGameManager.endGM.NextState();
    }
}
