using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePicker : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        int spriteIndex = Random.Range(0, sprites.Length);
        sr.sprite = sprites[spriteIndex];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
