using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public enum EndStates
    {
        TrumpDeath,
        TrumpDeathAction,
        TrumpDeathPost,
        MomAppearsAction,
        MomAppears,
        ComeTogetherAction,
        ComeTogether,
        DogJoinsAction,
        DogJoins,
        PortalOpensAction,
        PortalOpens,
        RelocateAction,
        Credit
    }

    public enum AvatarState
    {
        Dog,
        Mom,
        Boy,
        Trump
    }

    [Header("Animation Objects")]
    public GameObject momAppearsObjects;

    [Header("Dialogue Box Objects")]
    public Text avatarName;
    public GameObject dialogBox;
    public GameObject dogAvatarImage;
    public GameObject boyAvatarImage;
    public GameObject momAvatarImage;
    public GameObject trumpAvatarImage;
    public IntroDialogTextBox dialogText;

    [Header("Trump Death Dialogue File")]
    public TextAsset trumpDiesTrumpText;

    [Header("Trump Death Post Dialogue File")]
    public TextAsset trumpDiesPostDogText;

    [Header("Mom Appears Dialogue File")]
    public TextAsset momAppearsMomText;
    public TextAsset momAppearsBoyText;

    [Header("Come Together Dialogue File")]
    public TextAsset comeTogetherMomText;

    [Header("Dog Joins Dialogue File")]
    public TextAsset dogJoinsDogText;
    public TextAsset dogJoinsMomText;

    [Header("Portal Opens Dialogue File")]
    public TextAsset portalOpeningMomText;
    public TextAsset portalOpeningBoyText;

    private Fader fade;

    private bool dogDialogueCheck;
    private bool boyDialogueCheck;
    private bool momDialogueCheck;
    private bool trumpDialogueCheck;

    private bool textActive;

    private bool animationActive;

    public EndStates currentState;

    public static EndGameManager endGM;

    // Start is called before the first frame update
    void Awake()
    {
        endGM = this;

        fade = GetComponent<Fader>();

        dialogBox.SetActive(false);
        dogAvatarImage.SetActive(false);

        animationActive = false;

        textActive = false;
        currentState = EndStates.TrumpDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (currentState == EndStates.TrumpDeath)
                TrumpDeathState();
            else if (currentState == EndStates.TrumpDeathAction && !animationActive)
                TrumpDeathActionState();
            else if (currentState == EndStates.TrumpDeathPost)
                TrumpDeathPostState();
            else if (currentState == EndStates.MomAppearsAction && !animationActive)
                MomAppearsActionState();
            else if (currentState == EndStates.MomAppears)
                MomAppearsState();
            else if (currentState == EndStates.ComeTogetherAction && !animationActive)
                ComeTogetherActionState();
            else if (currentState == EndStates.ComeTogether)
                ComeTogetherState();
            else if (currentState == EndStates.DogJoinsAction && !animationActive)
                DogJoinsActionState();
            else if (currentState == EndStates.DogJoins)
                DogJoinsState();
            else if (currentState == EndStates.PortalOpensAction && !animationActive)
                PortalOpensActionState();
            else if (currentState == EndStates.PortalOpens)
                PortalOpensState();
            else if (currentState == EndStates.RelocateAction && !animationActive)
                RelocateActionState();
            else if (currentState == EndStates.Credit && !animationActive)
                CreditActionState();
        }
    }

    public void TrumpDeathState()
    {
        if (!trumpDialogueCheck)
        {
            startIntroDialogue(trumpDiesTrumpText, AvatarState.Trump, "N. Pres");
            trumpDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void TrumpDeathActionState()
    {
        // SCRIPTED DEATH

        animationActive = true;
    }

    public void TrumpDeathPostState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(trumpDiesPostDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void MomAppearsActionState()
    {
        momAppearsObjects.SetActive(true);

        animationActive = true;
    }

    public void MomAppearsState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(momAppearsMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(momAppearsBoyText, AvatarState.Boy, "Gundalf");
            boyDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void ComeTogetherActionState()
    {
        // SCRIPTED COME TOGETHER
        animationActive = true;
    }

    public void ComeTogetherState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(comeTogetherMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogJoinsActionState()
    {
        // SCRIPTED DOG JOINS
        animationActive = true;
    }

    public void DogJoinsState()
    {
        if (!dogDialogueCheck)
        {
            startIntroDialogue(dogJoinsDogText, AvatarState.Dog, "GunDog");
            dogDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void PortalOpensActionState()
    {
        animationActive = true;
    }

    public void PortalOpensState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(portalOpeningMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        if (!boyDialogueCheck)
        {
            startIntroDialogue(portalOpeningBoyText, AvatarState.Boy, "Gundalf");
            boyDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void RelocateActionState()
    {
        animationActive = true;
    }

    public void CreditActionState()
    {
        animationActive = true;
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

    private void ResetDialogueCheck()
    {
        dogDialogueCheck = false;
        boyDialogueCheck = false;
        momDialogueCheck = false;
        trumpDialogueCheck = false;
    }

    public void NextState()
    {
        if (currentState == EndStates.TrumpDeath)
            currentState = EndStates.TrumpDeathAction;
        else if (currentState == EndStates.TrumpDeathAction)
            currentState = EndStates.TrumpDeathPost;
        else if (currentState == EndStates.TrumpDeathPost)
            currentState = EndStates.MomAppearsAction;
        else if (currentState == EndStates.MomAppearsAction)
            currentState = EndStates.MomAppears;
        else if (currentState == EndStates.MomAppears)
            currentState = EndStates.ComeTogetherAction;
        else if (currentState == EndStates.ComeTogetherAction)
            currentState = EndStates.ComeTogether;
        else if (currentState == EndStates.ComeTogether)
            currentState = EndStates.DogJoinsAction;
        else if (currentState == EndStates.DogJoinsAction)
            currentState = EndStates.DogJoins;
        else if (currentState == EndStates.DogJoins)
            currentState = EndStates.PortalOpensAction;
        else if (currentState == EndStates.PortalOpensAction)
            currentState = EndStates.PortalOpens;
        else if (currentState == EndStates.PortalOpens)
            currentState = EndStates.RelocateAction;
        else if (currentState == EndStates.RelocateAction)
            currentState = EndStates.Credit;

        animationActive = false;
        ResetDialogueCheck();
    }
}
