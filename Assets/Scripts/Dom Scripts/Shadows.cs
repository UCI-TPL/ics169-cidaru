using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour {
    public Renderer objRenderer;
    void Awake () {
        objRenderer.receiveShadows = true;
        objRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }


	
}
