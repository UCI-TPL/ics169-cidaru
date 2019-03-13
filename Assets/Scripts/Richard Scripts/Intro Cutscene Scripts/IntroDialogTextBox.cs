using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogTextBox : MonoBehaviour
{
    public float setDelayTimer;

    public Animator dogAnimator;
    public Animator boyAnimator;
    public Animator momAnimator;
    public Animator trumpAnimator;

    public float setNextLineBuffer = 60f;

    private TextAsset textFile;
    private Text dialogBox;
    private string[] fileLines;
    private int currentLine;
    private float delayTimer;
    private bool dialogCoroutineStarted;
    private Coroutine textTyping;

    private IntroGameManager.AvatarState currentAvatar;
    private EndGameManager.AvatarState endCurrentAvatar;

    private float nextLineBuffer;

    private void Awake()
    {
        delayTimer = setDelayTimer;
        dialogBox = GetComponent<Text>();
        dialogCoroutineStarted = false;
        nextLineBuffer = setNextLineBuffer;
    }

    private void Update()
    {
        if (dialogCoroutineStarted)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Space))
            {
                StopCoroutine(textTyping);
                dialogBox.text = fileLines[currentLine];
                nextLineBuffer = setNextLineBuffer;
                dialogCoroutineStarted = false;
                currentLine++;
            }
        }
        else
        {
            if (nextLineBuffer > 0)
            {
                nextLineBuffer -= Time.unscaledDeltaTime;
            }
            else
            {
                if ((Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Space)) &&
                    currentLine < fileLines.Length)
                {
                    if (IntroGameManager.introGM != null)
                        textTyping = StartCoroutine(TextTyping());
                    else
                        textTyping = StartCoroutine(OutroGameManagerTextTyping());

                    dialogCoroutineStarted = true;
                }
            }
        }

        if (nextLineBuffer > 0)
        {
            nextLineBuffer -= Time.unscaledDeltaTime;
        }
        else
        {
            if (currentLine >= fileLines.Length && (Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Space)))
            {
                if (IntroGameManager.introGM != null)
                    IntroGameManager.introGM.endIntroDialogue();
                else
                    EndGameManager.endGM.endIntroDialogue();
            }
        }
    }

    public void startText(TextAsset txt, EndGameManager.AvatarState avatar)
    {
        textFile = txt;

        endCurrentAvatar = avatar;

        fileLines = (textFile.text.Split('\n'));

        currentLine = 0;

        dialogCoroutineStarted = true;

        textTyping = StartCoroutine(OutroGameManagerTextTyping());
    }

    public void startText(TextAsset txt, IntroGameManager.AvatarState avatar)
    {
        textFile = txt;

        currentAvatar = avatar;

        fileLines = (textFile.text.Split('\n'));

        currentLine = 0;

        dialogCoroutineStarted = true;

        textTyping = StartCoroutine(TextTyping());
    }

    IEnumerator TextTyping()
    {
        if (currentAvatar == IntroGameManager.AvatarState.Dog)
            dogAnimator.SetBool("talking", true);
        else if (currentAvatar == IntroGameManager.AvatarState.Trump)
            trumpAnimator.SetBool("talking", true);

        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSecondsRealtime(delayTimer);
        }

        currentLine++;
        nextLineBuffer = setNextLineBuffer;
        dialogCoroutineStarted = false;

        if (currentAvatar == IntroGameManager.AvatarState.Dog)
            dogAnimator.SetBool("talking", false);
        else if (currentAvatar == IntroGameManager.AvatarState.Trump)
            trumpAnimator.SetBool("talking", false);
    }

    IEnumerator OutroGameManagerTextTyping()
    {
        if (endCurrentAvatar == EndGameManager.AvatarState.Dog)
            dogAnimator.SetBool("talking", true);
        else if (endCurrentAvatar == EndGameManager.AvatarState.Trump)
            trumpAnimator.SetBool("talking", true);

        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSecondsRealtime(delayTimer);
        }

        currentLine++;
        nextLineBuffer = setNextLineBuffer;
        dialogCoroutineStarted = false;

        if (endCurrentAvatar == EndGameManager.AvatarState.Dog)
            dogAnimator.SetBool("talking", false);
        else if (endCurrentAvatar == EndGameManager.AvatarState.Trump)
            trumpAnimator.SetBool("talking", false);
    }
}
