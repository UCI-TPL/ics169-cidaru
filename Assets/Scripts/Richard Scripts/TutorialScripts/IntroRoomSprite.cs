using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroRoomSprite : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<HighlightRoom>().highlightRoomSprite();
    }
}
