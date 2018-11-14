using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {
    private ParticleSystem particle;

	// Use this for initialization
	void Awake () {
        particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!particle.isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
