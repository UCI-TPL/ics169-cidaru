using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePicker : MonoBehaviour {

    public Sprite[] sprites;

    public bool randomizeInitial = true;

    private SpriteRenderer sr;

	// Use this for initialization
	void Awake () {
        sr = GetComponent<SpriteRenderer>();

        if (randomizeInitial)
        {
            int spriteIndex = Random.Range(0, sprites.Length);
            sr.sprite = sprites[spriteIndex];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickVaseRubbleSprite(Sprite s)
    {
        if (s.name == "vase_0")
            sr.sprite = sprites[0];
        else
            sr.sprite = sprites[1];
    }
}
