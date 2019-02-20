﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimShadow : MonoBehaviour
{
    public SpriteRenderer parentRend;
    public  SpriteRenderer thisRend;


    void Start()
    {
        //parentRend = GetComponentInParent<SpriteRenderer>();
       // thisRend = GetComponent<SpriteRenderer>();
    }

 
    void Update()
    {
        thisRend.sprite = parentRend.sprite;
        thisRend.flipX = parentRend.flipX;
    }
}
