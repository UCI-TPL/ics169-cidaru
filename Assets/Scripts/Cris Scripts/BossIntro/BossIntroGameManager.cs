 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossIntroGameManager : MonoBehaviour
{
    /// Based on Richard's other cutscene Game Managers
    public enum BossIntroStates
    {
        Opening,
        EndIntro
    }

    public static BossIntroGameManager bossIntroGM;
    public BossIntroStates currentState;
    public GameObject introObjects;

    private bool happened;

    void Awake()
    {
        bossIntroGM = this;
        happened = false;
    }

    private void Update()
    {
        if (happened)
            currentState = BossIntroStates.EndIntro;
        if (Time.timeScale != 0f)
        {
            if (currentState == BossIntroStates.Opening)
                OpeningState();
            else if (currentState == BossIntroStates.EndIntro)
                EndIntroState();
        }
    }

    public void OpeningState()
    {
    }

    public void EndIntroState()
    {
        if (introObjects)
        {
            introObjects.GetComponent<Animator>().SetBool("close", true);
            Destroy(introObjects);
        }
        happened = true;
    }

    public void NextState()
    {
        if (currentState == BossIntroStates.Opening)
            currentState = BossIntroStates.EndIntro;
    }
}
