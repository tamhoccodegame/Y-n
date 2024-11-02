using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutGame : MonoBehaviour
{
	public CinemachineVirtualCamera cam;
	public float targetSize = 10f;  // Kích thước mong muốn khi zoom ra
	public float duration = 2f;  // Thời gian để hoàn thành hiệu ứng zoom
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			StartCoroutine(ZoomOutCamera());
		}
	}

	IEnumerator ZoomOutCamera()
	{
		float startSize = cam.m_Lens.OrthographicSize;  // Kích thước hiện tại của camera
		float elapsedTime = 0f;  // Thời gian đã trôi qua

		// Trong khoảng thời gian `duration`, camera sẽ thay đổi từ `startSize` đến `targetSize`
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			cam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
			yield return null;  // Chờ đến frame kế tiếp
		}

		// Đảm bảo camera đạt kích thước đích khi hoàn thành
		cam.m_Lens.OrthographicSize = targetSize;
	}
}
