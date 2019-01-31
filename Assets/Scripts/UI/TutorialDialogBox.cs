using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogBox : MonoBehaviour
{
    public float setDelayTimer;

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

    // Use this for initialization
    void Start()
    {
        dialogBox.text = "";
    }


    // Update is called once per frame
    void Update()
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
        } else
        {
            if ((Input.GetMouseButtonUp(0) || Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button")) && currentLine < fileLines.Length)
            {
                textTyping = StartCoroutine(TextTyping());
                dialogCoroutineStarted = true;
            }
        }


        if (currentLine >= fileLines.Length)
            GameManager.gm.endTutorialDialogue();
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
        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSecondsRealtime(delayTimer);
        }
    }
}
