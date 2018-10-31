using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystal : MonoBehaviour {

    public int manaAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && col.GetComponent<Mana>().getCurrentMana() != col.GetComponent<Mana>().maxMana)
        {
            col.GetComponent<Mana>().gainMana(manaAmount);
            Destroy(gameObject);
        }
    }
}
