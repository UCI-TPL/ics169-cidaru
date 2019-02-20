using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightRoom : MonoBehaviour
{
    public Sprite highlightSprite;

    private Sprite normalSprite;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;
    }

    public void highlightRoomSprite()
    {
        sr.sprite = highlightSprite;
    }

    public void resetRoom()
    {
        sr.sprite = normalSprite;
    }
}
