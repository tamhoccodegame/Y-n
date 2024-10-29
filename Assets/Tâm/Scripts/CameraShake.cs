using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Camera sẽ trở về vị trí ban đầu sau khi hết thời gian rung
	private Vector3 initialPosition;
	CinemachineBasicMultiChannelPerlin noise;
	private float initialAmplitude;
	private float initialFrequency;

	private void OnEnable()
	{
		// Lưu vị trí ban đầu của camera
		initialPosition = transform.localPosition;
	}

	private void Start()
	{
		CinemachineVirtualCamera virtualCam = GetComponent<CinemachineVirtualCamera>();
		if (virtualCam != null)
		{
			noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			initialAmplitude = noise.m_AmplitudeGain;
			initialFrequency = noise.m_FrequencyGain;
		}
	}

	public void ShakeCamera(float amplitude, float frequency, float duration)
	{
		if(noise != null)
		{
			noise.m_AmplitudeGain = amplitude;
			noise.m_FrequencyGain = frequency;
			StartCoroutine(StopShake(duration));
		}
	}

	IEnumerator StopShake(float duration)
	{
		yield return new WaitForSeconds(duration);

		if(noise != null)
		{
			noise.m_AmplitudeGain = initialAmplitude;
			noise.m_FrequencyGain = initialFrequency;
		}

		transform.localPosition = initialPosition;
	}

}
