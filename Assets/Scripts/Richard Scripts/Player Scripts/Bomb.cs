using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    // Timer til bomb explodes
    public float setBombTimer;

    // Current timer of the bomb
    protected float bombTimer;

    // Initializes the bomb timer
    public void Awake()
    {
        bombTimer = setBombTimer;
    }
}
