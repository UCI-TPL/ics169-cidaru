using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameManager : MonoBehaviour
{
    public enum IntroStates
    {
        Opening,
        DogEmerges,
        DogLeaves,
        EnemiesAppear,
        ActionPhase,
        TrumpAppears,
        TrumpShoots,
        TrumpLeaves,
        DogReemerges,
        MontageTime,
        EndIntro
    }

    public enum AvatarState
    {
        Dog,
        Mom,
        Boy,
        Trump
    }

    public static IntroGameManager introGM;

    [Header("Dialogue Box Objects")]
    public GameObject dialogBox;
    public GameObject dogAvatarImage;
    public GameObject boyAvatarImage;
    public GameObject momAvatarImage;
    public GameObject trumpAvatarImage;
    public IntroDialogTextBox dialogText;

    [Header("Opening Dialogue Files")]
    public TextAsset openingMomText;
    public TextAsset openingBoyText;

    [Header("Dog Emerges Dialogue Files")]
    public TextAsset dogEmergesBoyText;
    public TextAsset dogEmergesDogText;
    public TextAsset dogEmergesMomText;

    [Header("Dog Leaves Dialogue Files")]
    public TextAsset dogLeavesDogText;
    public TextAsset dogLeavesMomText;

    [Header("Enemies Appear Dialogue Files")]
    public TextAsset enemiesAppearMomText;

    [Header("Trump Appears Dialogue Files")]
    public TextAsset trumpAppearsTrumpText;
    public TextAsset trumpAppearsMomText;
    public TextAsset trumpAppearsTrumpTextP2;
    public TextAsset trumpAppearsMomTextP2;
    public TextAsset trumpAppearsTrumpTextP3;

    [Header("Trump Shoots Dialogue Files")]
    public TextAsset trumpShootMomText;
    public TextAsset trumpShootTrumpText;

    [Header("Dog Reemerges Dialogue Files")]
    public TextAsset dogReemergesDogText;

    [Header("Montage Time Dialogue Files")]
    public TextAsset montageTimeDogText;
    public TextAsset montageTimeBoyText;
    public TextAsset montageTimeDogTextP2;

    private bool textActive;

    private IntroStates currentState;

    // Dialogue Check
    private bool dogDialogueCheck;
    private bool boyDialogueCheck;
    private bool momDialogueCheck;
    private bool trumpDialogueCheck;
    private bool dogDialogueCheckP2;
    private bool momDialogueCheckP2;
    private bool trumpDialogueCheckP2;
    private bool trumpDialogueCheckP3;

    // Start is called before the first frame update
    void Awake()
    {
        introGM = this;

        dialogBox.SetActive(false);
        dogAvatarImage.SetActive(false);

        textActive = false;
        currentState = IntroStates.Opening;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (currentState == IntroStates.Opening)
                OpeningState();
            else if (currentState == IntroStates.DogEmerges)
                DogEmergesState();
            else if (currentState == IntroStates.DogLeaves)
                DogLeavesState();
            else if (currentState == IntroStates.EnemiesAppear)
                EnemiesAppearState();
            else if (currentState == IntroStates.ActionPhase)
                ActionPhaseState();
            else if (currentState == IntroStates.TrumpAppears)
                TrumpAppearsState();
            else if (currentState == IntroStates.TrumpShoots)
                TrumpShootsState();
            else if (currentState == IntroStates.TrumpLeaves)
                TrumpLeavesState();
            else if (currentState == IntroStates.DogReemerges)
                DogReemergesState();
            else if (currentState == IntroStates.MontageTime)
                MontageTimeState();
            else if (currentState == IntroStates.EndIntro)
                EndIntroState();
        }
    }

    public void OpeningState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(openingMomText);
            momDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(openingBoyText);
            boyDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogEmergesState()
    {
        if (!boyDialogueCheck)
        {
            startIntroDialogue(dogEmergesBoyText);
            boyDialogueCheck = true;
            return;
        }

        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogEmergesDogText);
            dogDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(dogEmergesMomText);
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogLeavesState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogLeavesDogText);
            dogDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(dogLeavesMomText);
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void EnemiesAppearState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(enemiesAppearMomText);
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void ActionPhaseState()
    {
        // FILL THIS UP WITH SCRIPTED ACTIONS

        NextState();
    }

    public void TrumpAppearsState()
    {
        if (!trumpDialogueCheck)
        {
            startIntroDialogue(trumpAppearsTrumpText);
            trumpDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(trumpAppearsMomText);
            momDialogueCheck = true;
            return;
        }

        if (!trumpDialogueCheckP2)
        {
            startIntroDialogue(trumpAppearsTrumpTextP2);
            trumpDialogueCheckP2 = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(trumpAppearsMomTextP2);
            momDialogueCheck = true;
            return;
        }

        if (!trumpDialogueCheckP3)
        {
            startIntroDialogue(trumpAppearsTrumpTextP3);
            trumpDialogueCheckP3 = true;
            return;
        }

        NextState();
    }

    public void TrumpShootsState()
    {
        if (!trumpDialogueCheck)
        {
            startIntroDialogue(trumpShootTrumpText);
            trumpDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void TrumpLeavesState()
    {
        // SCRIPTED ACTION

        NextState();
    }

    public void DogReemergesState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogReemergesDogText);
            dogDialogueCheck = true;
            return;
        }
    }

    public void MontageTimeState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(montageTimeDogText);
            dogDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(montageTimeDogText);
            boyDialogueCheck = true;
            return;
        }

        if (!dogDialogueCheckP2)
        {
            startIntroDialogue(montageTimeDogTextP2);
            dogDialogueCheckP2 = true;
            return;
        }

        NextState();
    }

    public void EndIntroState()
    {
        // SCRIPTED ACTION
    }

    public void startIntroDialogue(TextAsset text)
    {
        Time.timeScale = 0;

        dialogBox.SetActive(true);
        dogAvatarImage.SetActive(true);

        dialogText.startText(text);

        textActive = true;
    }

    public void endIntroDialogue()
    {
        Time.timeScale = 1;

        dialogBox.SetActive(false);
        dogAvatarImage.SetActive(false);
        
        textActive = false;
    }

    public void NextState()
    {
        if (currentState == IntroStates.Opening)
            currentState = IntroStates.DogEmerges;
        else if (currentState == IntroStates.DogEmerges)
            currentState = IntroStates.DogLeaves;
        else if (currentState == IntroStates.DogLeaves)
            currentState = IntroStates.EnemiesAppear;
        else if (currentState == IntroStates.EnemiesAppear)
            currentState = IntroStates.ActionPhase;
        else if (currentState == IntroStates.ActionPhase)
            currentState = IntroStates.TrumpAppears;
        else if (currentState == IntroStates.TrumpAppears)
            currentState = IntroStates.TrumpShoots;
        else if (currentState == IntroStates.TrumpShoots)
            currentState = IntroStates.TrumpLeaves;
        else if (currentState == IntroStates.TrumpLeaves)
            currentState = IntroStates.DogReemerges;
        else if (currentState == IntroStates.DogReemerges)
            currentState = IntroStates.EndIntro;
    }

    private void ResetDialogueCheck()
    {
        dogDialogueCheck = false;
        boyDialogueCheck = false;
        momDialogueCheck = false;
        trumpDialogueCheck = false;
        dogDialogueCheckP2 = false;
        momDialogueCheckP2 = false;
        trumpDialogueCheckP3 = false;
    }
}
