using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextBox : MonoBehaviour {
    public float setDelayTimer;
    public float setVisibleTimer;

    private TextAsset textFile;
    private Text dialogBox;
    private string[] fileLines;
    private int currentLine;
    private float delayTimer;
    private float visibleTimer;

    private void Awake()
    {
        delayTimer = setDelayTimer;
        visibleTimer = setVisibleTimer;
        dialogBox = GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        dialogBox.text = "";
	}
	
    public void startText(TextAsset txt)
    {
        textFile = txt;

        fileLines = (textFile.text.Split('\n'));

        StartCoroutine(TextTyping());
    }

    IEnumerator TextTyping()
    {
        dialogBox.text = "";
        for (int i = 0; i < fileLines[currentLine].Length; i++)
        {
            dialogBox.text += fileLines[currentLine][i];
            yield return new WaitForSeconds(delayTimer);
        }

        yield return new WaitForSeconds(visibleTimer);
        dialogBox.text = "";

        GameManager.gm.endDialogue();
    }
}
