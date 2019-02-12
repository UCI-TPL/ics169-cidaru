using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextBox : MonoBehaviour {
    public float setDelayTimer;
    public Animator avatarImage;

    private TextAsset textFile;
    private Text dialogBox;
    private string[] fileLines;
    private int currentLine;
    private float delayTimer;
    private bool dialogCoroutineStarted;
    private Coroutine textTyping;

    private void Awake()
    {
        delayTimer = setDelayTimer;
        dialogBox = GetComponent<Text>();
        dialogCoroutineStarted = false;
    }

    private void Update()
    {
        if (dialogCoroutineStarted)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button"))
            {
                StopCoroutine(textTyping);
                dialogBox.text = fileLines[currentLine];
                dialogCoroutineStarted = false;
                currentLine++;
            }
        }
        else
        {
            if ((Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button")) && currentLine < fileLines.Length)
            {
                textTyping = StartCoroutine(TextTyping());
                dialogCoroutineStarted = true;
            }
        }

        if (currentLine >= fileLines.Length && (Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button")) && GameManager.gm.isTutorial)
            GameManager.gm.endTutorialDialogue();
        else if (currentLine >= fileLines.Length && (Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button")) && !GameManager.gm.isTutorial)
            GameManager.gm.endDialogue();
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
        avatarImage.SetBool("talking", true);

        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSecondsRealtime(delayTimer);
        }

        currentLine++;
        dialogCoroutineStarted = false;
        avatarImage.SetBool("talking", false);

    }
}
