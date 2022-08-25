using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	CinemachineVirtualCamera cmVC;

	float shakeTimer;

	private void Awake()
	{
		cmVC = GetComponent<CinemachineVirtualCamera>();
	}

	public void ShakeCamera(float intensity, float time)
	{
		CinemachineBasicMultiChannelPerlin cmPerlin = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		cmPerlin.m_AmplitudeGain = intensity;
		shakeTimer = time;
	}

	private void Update()
	{
		if (shakeTimer > 0)
		{
			shakeTimer -= Time.deltaTime;
			if (shakeTimer <= 0f)
			{
				CinemachineBasicMultiChannelPerlin cmPerlin = cmVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
				cmPerlin.m_AmplitudeGain = 0f;
			}
		}
	}
}
