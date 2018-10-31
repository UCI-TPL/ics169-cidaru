using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {

    public int maxMana;
    public float regenTime;

    private int currentMana;
    private bool regening;

    // Use this for initialization
    void Start () {
        currentMana = maxMana;
        regening = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentMana != maxMana && !regening)
        {
            StartCoroutine(ManaRegen());
        }
	}

    public int getCurrentMana()
    {
        return currentMana;
    }

    public void useMana(int m)
    {
        currentMana -= m;
    }

    public void gainMana(int m)
    {
        if (currentMana + m >= maxMana)
        {
            currentMana = maxMana;
        } else
        {
            currentMana += m;
        }
    }

    private IEnumerator ManaRegen()
    {
        regening = true;

        while (currentMana < maxMana)
        {
            currentMana++;

            yield return new WaitForSeconds(regenTime);
        }

        regening = false;


    }
}
