using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontColorController : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    public Button optionsButton;
    public Button quitButton;

    public TextMeshProUGUI continueText;
    public TextMeshProUGUI newGameText;
    public TextMeshProUGUI optionsText;
    public TextMeshProUGUI quitText;

    public void Update()
    {
        if (continueButton.IsInteractable())
            continueText.color = new Color(continueText.color.r, continueText.color.g, continueText.color.b, 1);
        else
            continueText.color = new Color(continueText.color.r, continueText.color.g, continueText.color.b, 0.2f);

        if (newGameButton.IsInteractable())
            newGameText.color = new Color(newGameText.color.r, newGameText.color.g, newGameText.color.b, 1);
        else
            newGameText.color = new Color(newGameText.color.r, newGameText.color.g, newGameText.color.b, 0.2f);

        if (optionsButton.IsInteractable())
            optionsText.color = new Color(optionsText.color.r, optionsText.color.g, optionsText.color.b, 1);
        else
            optionsText.color = new Color(optionsText.color.r, optionsText.color.g, optionsText.color.b, 0.2f);

        if (quitButton.IsInteractable())
            quitText.color = new Color(quitText.color.r, quitText.color.g, quitText.color.b, 1);
        else
            quitText.color = new Color(quitText.color.r, quitText.color.g, quitText.color.b, 0.2f);
    }
}
