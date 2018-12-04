using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour {
    public float setShakeDuration = 1f;
    public float shakeAmp = 3f;
    public float shakeFreq = 2f;

    public CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private float shakeDuration;
    private bool shaking = false;

    private void Awake()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        virtualCameraNoise.m_AmplitudeGain = 0;
        virtualCameraNoise.m_FrequencyGain = shakeFreq;
        shaking = false;
    }

    // Update is called once per frame
    void Update () {
		if (shaking)
        {
            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0)
            {
                virtualCameraNoise.m_AmplitudeGain = 0;

                shaking = false;
            }
        }
	}

    public void startShake()
    {
        shakeDuration = setShakeDuration;
        virtualCameraNoise.m_AmplitudeGain = shakeAmp;

        shaking = true;
    }
}
