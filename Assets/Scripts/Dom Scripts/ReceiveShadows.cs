using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveShadows : MonoBehaviour {
    public Renderer tileRenderer;
    void Awake()
    {
        tileRenderer.receiveShadows = true;
        tileRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

}
