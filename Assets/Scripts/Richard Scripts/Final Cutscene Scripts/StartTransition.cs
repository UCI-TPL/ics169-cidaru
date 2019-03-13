using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTransition : MonoBehaviour
{
    public Transition transition;

    public GameObject roomBackground;
    public GameObject troyBackground;

    public GameObject postObjects;

    public void ActivateEndTransition()
    {
        StartCoroutine(LoadNewBackground());
    }

    IEnumerator LoadNewBackground()
    {
        transition.fadeToBlack();

        yield return new WaitForSecondsRealtime(1.2f);

        roomBackground.SetActive(true);
        troyBackground.SetActive(false);

        transition.unfadeFromBlack();

        postObjects.SetActive(true);
    }
}
