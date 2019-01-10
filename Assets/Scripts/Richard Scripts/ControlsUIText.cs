using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUIText : MonoBehaviour {

    public Text sprintText;
    public Text babyText;
    public Text slowText;
    public Text vortexText;
    public Text mapText;

    public void controllerText()
    {
        sprintText.text = "LT/A";
        babyText.text = "Y  ";
        slowText.text = "B";
        vortexText.text = "RB";
        mapText.text = "View";
    }

    public void keyboardText()
    {
        sprintText.text = "LShift";
        babyText.text = "     Q       ";
        slowText.text = "E";
        vortexText.text = "RMouse";
        mapText.text = "Tab";
    }
}
