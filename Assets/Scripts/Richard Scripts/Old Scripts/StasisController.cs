using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisController : MonoBehaviour {
    public enum Stasis
    {
        Normal,
        Burned,
        Stun
    }

    // BURN VARIABLES
    private float setBurnTickTimer;
    private float burnTickTimer;
    private int burnTickAmount;

    // STUN VARIABLES
    private float setStunTimer;
    private float stunTimer;

    private List<Stasis> currentStasis = new List<Stasis>();

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
            if (burnTickAmount != 0)
            {
                burnTickTimer -= Time.deltaTime;

                if (burnTickTimer <= 0f)
                {
                    hp.TakeDamage(1);
                    burnTickTimer = setBurnTickTimer;
                    burnTickAmount--;
                }
            } else
            {
                currentStasis.Remove(Stasis.Burned);
                sprite.color -= Color.red;
            }
        }

        if (currentStasis.Contains(Stasis.Stun))
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                currentStasis.Remove(Stasis.Stun);
                sprite.color -= Color.yellow;
            }
        }
	}

    public void startBurn(float stt, int tAmt) // stt = stasisTickTimer, tAmt = tickAmount
    {
        if (!currentStasis.Contains(Stasis.Burned))
        {
            setBurnTickTimer = stt;
            burnTickTimer = stt;
            currentStasis.Add(Stasis.Burned);

            sprite.color += Color.red;
        }

        burnTickAmount = tAmt;
    }

    public void startStun(float st) // st = stunTimer
    {
        if (!currentStasis.Contains(Stasis.Stun))
        {
            setStunTimer = st;
            stunTimer = st;
            currentStasis.Add(Stasis.Stun);

            sprite.color += Color.yellow;
        }
    }

    public bool stunned()
    {
        return currentStasis.Contains(Stasis.Stun);
    }
}
