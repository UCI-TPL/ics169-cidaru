using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public float setBombTimer;

    protected float bombTimer;

    public void Awake()
    {
        bombTimer = setBombTimer;
    }
}
