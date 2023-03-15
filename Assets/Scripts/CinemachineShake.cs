using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : Singleton<CinemachineShake>
{

    public CinemachineBasicMultiChannelPerlin cmCameraPerlin;

    private float maxShake = -1f;
    private float shakeTime;

    private float remainingTime;

    void Start() {
        cmCameraPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0) {
            remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0f);
            cmCameraPerlin.m_AmplitudeGain = Mathf.Lerp(maxShake, 0f, (shakeTime - remainingTime) / shakeTime);
            cmCameraPerlin.m_FrequencyGain = Mathf.Lerp(maxShake, 0f, (shakeTime - remainingTime) / shakeTime);
        }
        else {
            maxShake = -1f;
        }
    }

    public void SetShake(float shakeAmount, float time) {
        if(shakeAmount > maxShake) {
            maxShake = shakeAmount;
            shakeTime = time;
            remainingTime = time;
        }
    }
}
