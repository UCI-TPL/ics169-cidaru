using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextIntroState : MonoBehaviour
{
    public void ChangeToNextState()
    {
        IntroGameManager.introGM.NextState();
    }
}
