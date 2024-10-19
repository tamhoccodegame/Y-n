using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	public CinemachineVirtualCamera cam;
	public float zoomSpeed = 2f;
	public float targetFOV = 30f;
	public float originalFOV = 60f;

	void Start()
	{
		cam.m_Lens.FieldOfView = originalFOV; // Set original FOV
	}

	void Update()
	{
		// Zoom In
		if (Input.GetKeyDown(KeyCode.Z))
		{
			StartCoroutine(ZoomIn());
		}
		// Zoom Out
		if (Input.GetKeyDown(KeyCode.X))
		{
			StartCoroutine(ZoomOut());
		}
	}

	private IEnumerator ZoomIn()
	{
		while (cam.m_Lens.FieldOfView > targetFOV)
		{
			cam.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator ZoomOut()
	{
		while (cam.m_Lens.FieldOfView < originalFOV)
		{
			cam.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime;
			yield return null;
		}
	}
}
