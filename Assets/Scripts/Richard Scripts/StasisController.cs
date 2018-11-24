using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisController : MonoBehaviour {
    public enum Stasis
    {
        Normal,
        Burned
    }

    private float setStasisTickTimer;

    private float stasisTickTimer;
    private int tickAmount;
    private List<Stasis> currentStasis;

    private Health hp;

    private SpriteRenderer sprite;

    private void Awake()
    {
        hp = GetComponent<Health>();

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		if (currentStasis.Contains(Stasis.Burned))
        {
            if (tickAmount != 0)
            {
                stasisTickTimer -= Time.deltaTime;

                if (stasisTickTimer <= 0f)
                {
                    hp.TakeDamage(1);
                    stasisTickTimer = setStasisTickTimer;
                    tickAmount--;
                }
            } else
            {
                currentStasis.Remove(Stasis.Burned);
                sprite.color = Color.white;
            }
        }
	}

    public void startBurn(float stt, int tAmt) // stt = stasisTickTimer, tAmt = tickAmount
    {
        if (!currentStasis.Contains(Stasis.Burned))
        {
            setStasisTickTimer = stt;
            stasisTickTimer = stt;
            currentStasis.Add(Stasis.Burned);
        }

        tickAmount = tAmt;

        sprite.color += Color.red;
    }
}
