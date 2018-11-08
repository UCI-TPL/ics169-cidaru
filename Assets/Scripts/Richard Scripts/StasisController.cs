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
    private Stasis currentStasis;

    private Health hp;

    private SpriteRenderer sprite;

    private void Awake()
    {
        currentStasis = Stasis.Normal;

        hp = GetComponent<Health>();

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		if (currentStasis == Stasis.Burned)
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
                currentStasis = Stasis.Normal;
                sprite.color = Color.white;
            }
        }
	}

    public void setStasisProperties(Stasis s, float stt, int tAmt) // stt = stasisTickTimer, tAmt = tickAmount
    {
        if (currentStasis != s)
        {
            setStasisTickTimer = stt;
            stasisTickTimer = stt;
        }

        currentStasis = s;
        tickAmount = tAmt;

        sprite.color = Color.red;
    }
}
