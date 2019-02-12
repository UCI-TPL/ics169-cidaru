using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconController : MonoBehaviour
{
    public Sprite keyboardSprite;
    public Sprite controllerSprite;

    private Image image;

    public void Awake()
    {
        image = GetComponent<Image>();

        if (PlayerPrefs.GetInt("Mouse") != 0)
            image.sprite = keyboardSprite;
        else
            image.sprite = controllerSprite;
    }
}
