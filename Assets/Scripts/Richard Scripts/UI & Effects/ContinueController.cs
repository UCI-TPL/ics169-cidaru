using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueController : MonoBehaviour
{
    public Button continueButton;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        int currentLevel = PlayerPrefs.GetInt("Level");

        if (currentLevel == 1)
            continueButton.interactable = false;
        else
            continueButton.interactable = true;
    }
}
