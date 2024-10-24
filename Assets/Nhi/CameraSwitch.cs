using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
	public float middleGroundZ = -5f; // Vị trí Z của middleground
	public float backgroundZ = -10f; // Vị trí Z của background
	public float temporaryZoomInSize = 8f; // Kích thước camera tạm thời khi zoom in
	public float temporaryZoomOutSize = 12f; // Kích thước camera tạm thời khi zoom out
	public float zoomSpeed = 2f; // Tốc độ chuyển đổi lớp
	public float recoverSpeed = 20f;

	private Camera cam;
	private int originalSize; // Kích thước ban đầu của camera

	void Start()
	{
		cam = Camera.main;
		cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, middleGroundZ);
		originalSize = (int)cam.orthographicSize; // Lưu kích thước ban đầu
	}

	void Update()
	{
		// Kiểm tra vị trí của nhân vật để chuyển đổi camera
		if (Input.GetKeyDown(KeyCode.Z)) // Điều kiện chuyển sang background
		{
			StartCoroutine(ZoomIn(backgroundZ));
		}
		else if (Input.GetKeyDown(KeyCode.U)) // Điều kiện chuyển về middleground
		{
			StartCoroutine(ZoomOut(middleGroundZ));
		}
	}

	private IEnumerator ZoomOut(float targetZ)
	{
		float elapsedTime = 0f;
		Vector3 startPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, backgroundZ);
		Vector3 targetPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, targetZ);

			
		while (elapsedTime < zoomSpeed)
		{
			cam.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomSpeed);
			cam.orthographicSize = Mathf.Lerp(temporaryZoomOutSize, originalSize, elapsedTime / zoomSpeed);

			elapsedTime += Time.deltaTime;
			yield return null; // Chờ đến khung hình tiếp theo
		}

		//while (elapsedTime < recoverSpeed)
		//{
		//	cam.orthographicSize = Mathf.Lerp(originalSize, temporaryZoomOutSize, elapsedTime / zoomSpeed);
		//	elapsedTime += Time.deltaTime;
		//	yield return null;
		//}


		cam.transform.position = targetPosition; // Đảm bảo camera có vị trí chính xác
	}

	private IEnumerator ZoomIn(float targetZ)
	{
		float elapsedTime = 0f;
		Vector3 startPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, middleGroundZ);
		Vector3 targetPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, targetZ);

		while (elapsedTime < zoomSpeed)
		{
			cam.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomSpeed);
			cam.orthographicSize = Mathf.Lerp(originalSize, temporaryZoomInSize, elapsedTime / zoomSpeed);

			elapsedTime += Time.deltaTime;
			yield return null; // Chờ đến khung hình tiếp theo
		}
		elapsedTime = 0;

		while (cam.orthographicSize < originalSize)
		{
			cam.orthographicSize += 8 * Time.deltaTime;
			//elapsedTime += Time.deltaTime;
			yield return null;
		}


		//cam.orthographicSize = originalSize;
		cam.transform.position = targetPosition; // Đảm bảo camera có vị trí chính xác
	}
}
