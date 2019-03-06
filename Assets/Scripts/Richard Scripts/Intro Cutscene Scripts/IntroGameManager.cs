using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroGameManager : MonoBehaviour
{
    public enum IntroStates
    {
        Opening,
        DogEmergesAction,
        DogEmerges,
        DogLeavesAction,
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

    [Header("Animation Objects")]
    public GameObject dogEmergesObjects;

    [Header("Dialogue Box Objects")]
    public Text avatarName;
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

    private bool animationActive;

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

        animationActive = false;

        dogEmergesObjects.SetActive(false);

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
            else if (currentState == IntroStates.DogEmergesAction)
                DogEmergesActionState();
            else if (currentState == IntroStates.DogEmerges && !animationActive)
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
            startIntroDialogue(openingMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(openingBoyText, AvatarState.Boy, "Gundalf");
            boyDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogEmergesActionState()
    {
        dogEmergesObjects.SetActive(true);

        animationActive = true;
    }

    public void DogEmergesState()
    {
        if (!boyDialogueCheck)
        {
            startIntroDialogue(dogEmergesBoyText, AvatarState.Boy, "Gundalf");
            boyDialogueCheck = true;
            return;
        }

        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogEmergesDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(dogEmergesMomText, AvatarState.Mom, "MomDalf");
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogLeavesState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogLeavesDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(dogLeavesMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void EnemiesAppearState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(enemiesAppearMomText, AvatarState.Mom, "Momdalf");
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
            startIntroDialogue(trumpAppearsTrumpText, AvatarState.Trump, "???");
            trumpDialogueCheck = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(trumpAppearsMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        if (!trumpDialogueCheckP2)
        {
            startIntroDialogue(trumpAppearsTrumpTextP2, AvatarState.Trump, "N. Pres");
            trumpDialogueCheckP2 = true;
            return;
        }

        if (!momDialogueCheck)
        {
            startIntroDialogue(trumpAppearsMomTextP2, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        if (!trumpDialogueCheckP3)
        {
            startIntroDialogue(trumpAppearsTrumpTextP3, AvatarState.Trump, "N. Pres");
            trumpDialogueCheckP3 = true;
            return;
        }

        NextState();
    }

    public void TrumpShootsState()
    {
        if (!trumpDialogueCheck)
        {
            startIntroDialogue(trumpShootTrumpText, AvatarState.Trump, "N. Pres");
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
            startIntroDialogue(dogReemergesDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void MontageTimeState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(montageTimeDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(montageTimeBoyText, AvatarState.Boy, "Gundalf");
            boyDialogueCheck = true;
            return;
        }

        if (!dogDialogueCheckP2)
        {
            startIntroDialogue(montageTimeDogTextP2, AvatarState.Dog, "GunDog");
            dogDialogueCheckP2 = true;
            return;
        }

        NextState();
    }

    public void EndIntroState()
    {
        // SCRIPTED ACTION
    }

    public void startIntroDialogue(TextAsset text, AvatarState avatar, string name)
    {
        Time.timeScale = 0;

        dialogBox.SetActive(true);
        ActivateAvatar(avatar);
        avatarName.text = name;

        dialogText.startText(text, avatar);

        textActive = true;
    }

    public void endIntroDialogue()
    {
        Time.timeScale = 1;

        dialogBox.SetActive(false);
        DeactivateAvatar();
        
        textActive = false;
    }

    public void NextState()
    {
        if (currentState == IntroStates.Opening)
            currentState = IntroStates.DogEmergesAction;
        else if (currentState == IntroStates.DogEmergesAction)
            currentState = IntroStates.DogEmerges;
        else if (currentState == IntroStates.DogEmerges)
            currentState = IntroStates.DogLeavesAction;
        else if (currentState == IntroStates.DogLeavesAction)
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
            currentState = IntroStates.MontageTime;
        else if (currentState == IntroStates.MontageTime)
            currentState = IntroStates.EndIntro;

        animationActive = false;
        ResetDialogueCheck();
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

    private void ActivateAvatar(AvatarState avatar)
    {
        if (avatar == AvatarState.Boy)
            boyAvatarImage.SetActive(true);
        else if (avatar == AvatarState.Dog)
            dogAvatarImage.SetActive(true);
        else if (avatar == AvatarState.Mom)
            momAvatarImage.SetActive(true);
        else if (avatar == AvatarState.Trump)
            trumpAvatarImage.SetActive(true);
    }

    private void DeactivateAvatar()
    {
        boyAvatarImage.SetActive(false);
        dogAvatarImage.SetActive(false);
        momAvatarImage.SetActive(false);
        trumpAvatarImage.SetActive(false);
    }
}
