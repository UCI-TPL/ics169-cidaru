using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedNextState : MonoBehaviour
{
    public void DelayNextState()
    {
        StartCoroutine(CountdownNextState());
    }

    IEnumerator CountdownNextState()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        IntroGameManager.introGM.NextState();
    }
}
