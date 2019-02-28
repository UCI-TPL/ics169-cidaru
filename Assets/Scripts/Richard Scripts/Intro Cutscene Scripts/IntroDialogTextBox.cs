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
                print(nextLineBuffer);
            }
            else
            {
                if ((Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.Space)) &&
                    currentLine < fileLines.Length)
                {
                    textTyping = StartCoroutine(TextTyping());
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
                IntroGameManager.introGM.endIntroDialogue();
        }
    }

    public void startText(TextAsset txt)
    {
        textFile = txt;

        fileLines = (textFile.text.Split('\n'));

        currentLine = 0;

        dialogCoroutineStarted = true;

        textTyping = StartCoroutine(TextTyping());
    }

    IEnumerator TextTyping()
    {
        dogAnimator.SetBool("talking", true);

        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSecondsRealtime(delayTimer);
        }

        currentLine++;
        nextLineBuffer = setNextLineBuffer;
        dialogCoroutineStarted = false;
        dogAnimator.SetBool("talking", false);

    }
}
