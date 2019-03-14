using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroGameManager : MonoBehaviour
{
    public enum IntroStates
    {
        Opening,
        DogEmergesAction,
        DogEmerges,
        DogLeavesAction,
        DogLeaves,
        EnemiesAppearAction,
        EnemiesAppear,
        ActionPhase,
        TrumpAppearsAction,
        TrumpAppears,
        TrumpShootsAction,
        TrumpShoots,
        TrumpPostShootsAction,
        TrumpPostShoots,
        TrumpLeaves,
        DogReemergesAction,
        DogReemerges,
        MontageTimeAction,
        MontageTime,
        EndIntroAction,
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
    public Animator dogEmergesAnim;
    public Animator boyLeavesAnim;
    public GameObject backGunStaff;
    public GameObject frontGunStaff;
    public GameObject vortexSpawner;
    public GameObject trumpAppearsObjects;
    public Animator trumpShootsAnim;
    public IntroMomHitControl momHitControl;
    public Animator momAnim;
    public GameObject droppedStaff;
    public GameObject bossExitPortal;
    public GameObject dogReemergesObjects;
    public Animator gundalfReemergesAnim;

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

    public IntroStates currentState;

    //private Fader fade;
    public Transition transition;
    public AudioClip momSong;

    private bool fadingMusic;
    private bool unfadingMusic;
    private float timer;
    private AudioSource audioSource;

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

        //fade = GetComponent<Fader>();

        dialogBox.SetActive(false);
        dogAvatarImage.SetActive(false);

        animationActive = false;

        dogEmergesObjects.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        fadingMusic = false;
        unfadingMusic = false;

        textActive = false;
        currentState = IntroStates.Opening;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            currentState = IntroStates.EndIntro;
            Time.timeScale = 1f;
        }

        if (fadingMusic)
        {
            timer += Time.unscaledDeltaTime / 2f;
            audioSource.volume = Mathf.Lerp(0.5f, 0f, timer);

            if (audioSource.volume <= 0f)
            {
                fadingMusic = false;
                audioSource.Stop();
            }
        }

        if (unfadingMusic)
        {
            timer += Time.unscaledDeltaTime / 2f;
            audioSource.volume = Mathf.Lerp(0f, 0.5f, timer);

            if (audioSource.volume >= 0.5f)
                unfadingMusic = false;
        }
            
        if (Time.timeScale != 0f)
        {
            if (currentState == IntroStates.Opening)
                OpeningState();
            else if (currentState == IntroStates.DogEmergesAction)
                DogEmergesActionState();
            else if (currentState == IntroStates.DogEmerges && !animationActive)
                DogEmergesState();
            else if (currentState == IntroStates.DogLeavesAction && !animationActive)
                DogLeavesActionState();
            else if (currentState == IntroStates.DogLeaves)
                DogLeavesState();
            else if (currentState == IntroStates.EnemiesAppearAction && !animationActive)
                EnemiesAppearActionState();
            else if (currentState == IntroStates.EnemiesAppear)
                EnemiesAppearState();
            else if (currentState == IntroStates.ActionPhase && !animationActive)
                ActionPhaseState();
            else if (currentState == IntroStates.TrumpAppearsAction && !animationActive)
                TrumpAppearsActionState();
            else if (currentState == IntroStates.TrumpAppears)
                TrumpAppearsState();
            else if (currentState == IntroStates.TrumpShootsAction && !animationActive)
                TrumpShootsActionState();
            else if (currentState == IntroStates.TrumpShoots)
                TrumpShootsState();
            else if (currentState == IntroStates.TrumpPostShootsAction && !animationActive)
                TrumpPostShootsActionState();
            else if (currentState == IntroStates.TrumpPostShoots)
                TrumpPostShootsState();
            else if (currentState == IntroStates.TrumpLeaves && !animationActive)
                TrumpLeavesState();
            else if (currentState == IntroStates.DogReemergesAction && !animationActive)
                DogReemergesActionState();
            else if (currentState == IntroStates.DogReemerges)
                DogReemergesState();
            else if (currentState == IntroStates.MontageTimeAction && !animationActive)
                MontageTimeActionState();
            else if (currentState == IntroStates.MontageTime)
                MontageTimeState();
            else if (currentState == IntroStates.EndIntroAction && !animationActive)
                EndIntroActionState();
            else if (currentState == IntroStates.EndIntro && Time.timeScale != 0f)
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
        timer = 0f;
        fadingMusic = true;

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
            audioSource.clip = momSong;
            timer = 0f;
            unfadingMusic = true;
            audioSource.Play();
            startIntroDialogue(dogEmergesMomText, AvatarState.Mom, "MomDalf");
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void DogLeavesActionState()
    {
        dogEmergesAnim.SetBool("leave", true);

        animationActive = true;
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

    public void EnemiesAppearActionState()
    {
        dogEmergesAnim.SetBool("disappear", true);
        boyLeavesAnim.SetBool("leave", true);

        animationActive = true;
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
        backGunStaff.SetActive(false);
        frontGunStaff.SetActive(true);
        vortexSpawner.SetActive(true);

        animationActive = true;
    }

    public void TrumpAppearsActionState()
    {
        trumpAppearsObjects.SetActive(true);

        animationActive = true;
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

        if (!momDialogueCheckP2)
        {
            startIntroDialogue(trumpAppearsMomTextP2, AvatarState.Mom, "Momdalf");
            momDialogueCheckP2 = true;
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

    public void TrumpShootsActionState()
    {
        trumpShootsAnim.SetBool("shoot", true);

        animationActive = true;
    }

    public void TrumpShootsState()
    {
        if (!momDialogueCheck)
        {
            startIntroDialogue(trumpShootMomText, AvatarState.Mom, "Momdalf");
            momDialogueCheck = true;
            return;
        }

        NextState();
    }

    public void TrumpPostShootsActionState()
    {
        momHitControl.enabled = true;
        frontGunStaff.SetActive(false);
        droppedStaff.SetActive(true);
        momAnim.SetBool("hit", true);

        animationActive = true;
    }

    public void TrumpPostShootsState()
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
        bossExitPortal.SetActive(true);

        animationActive = true;
    }

    public void DogReemergesActionState()
    {
        dogReemergesObjects.SetActive(true);

        animationActive = true;
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

    public void MontageTimeActionState()
    {
        gundalfReemergesAnim.SetBool("move", true);

        animationActive = true;
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

    public void EndIntroActionState()
    {
        gundalfReemergesAnim.SetBool("move2", true);

        animationActive = true;
    }

    public void EndIntroState()
    {
        PlayerPrefs.SetInt("Level", 2);
        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex + 1));
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
            currentState = IntroStates.EnemiesAppearAction;
        else if (currentState == IntroStates.EnemiesAppearAction)
            currentState = IntroStates.EnemiesAppear;
        else if (currentState == IntroStates.EnemiesAppear)
            currentState = IntroStates.ActionPhase;
        else if (currentState == IntroStates.ActionPhase)
            currentState = IntroStates.TrumpAppearsAction;
        else if (currentState == IntroStates.TrumpAppearsAction)
            currentState = IntroStates.TrumpAppears;
        else if (currentState == IntroStates.TrumpAppears)
            currentState = IntroStates.TrumpShootsAction;
        else if (currentState == IntroStates.TrumpShootsAction)
            currentState = IntroStates.TrumpShoots;
        else if (currentState == IntroStates.TrumpShoots)
            currentState = IntroStates.TrumpPostShootsAction;
        else if (currentState == IntroStates.TrumpPostShootsAction)
            currentState = IntroStates.TrumpPostShoots;
        else if (currentState == IntroStates.TrumpPostShoots)
            currentState = IntroStates.TrumpLeaves;
        else if (currentState == IntroStates.TrumpLeaves)
            currentState = IntroStates.DogReemergesAction;
        else if (currentState == IntroStates.DogReemergesAction)
            currentState = IntroStates.DogReemerges;
        else if (currentState == IntroStates.DogReemerges)
            currentState = IntroStates.MontageTimeAction;
        else if (currentState == IntroStates.MontageTimeAction)
            currentState = IntroStates.MontageTime;
        else if (currentState == IntroStates.MontageTime)
            currentState = IntroStates.EndIntroAction;
        else if (currentState == IntroStates.EndIntroAction)
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

    IEnumerator FadeWait(int sceneIndex)
    {
        //float fadeTime = fade.BeginSceneFade(1);
        //fade.BeginAudioFade(1);

        transition.fadeToBlack();

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneIndex);
    }
}
