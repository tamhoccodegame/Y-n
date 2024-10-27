using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GroundSwitcher : MonoBehaviour
{
    public float backGroundY;
    public float middleGroundY;

    public Vector3 playerMiddleGroundScale;
    public Vector3 playerBackGroundScale;

    public SpriteRenderer playerOrderLayer;
	public CinemachineVirtualCamera virtualCamera;

	public readonly float middleGroundOrthographic = 7.5f;
	public readonly float backGroundOrthographic = 6;

	Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
			StartCoroutine(MoveYOverTime(backGroundY, false));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
			playerOrderLayer.sortingOrder = 0;
			StartCoroutine(MoveYOverTime(middleGroundY, true));
		}

	}

	private IEnumerator MoveYOverTime(float tagertY, bool isScaleUp)
	{
		float startY = transform.position.y; // Lấy vị trí Y ban đầu
		float elapsedTime = 0f; // Thời gian đã trôi qua
		Vector3 startScale = transform.localScale;
		Vector3 endScale = isScaleUp ? playerMiddleGroundScale : playerBackGroundScale;
		float startOrthographic = virtualCamera.m_Lens.OrthographicSize;
		float endOrthographic = isScaleUp ? middleGroundOrthographic : backGroundOrthographic;
		float timer = 1f;

		endScale.x *= Mathf.Sign(transform.localScale.x); //Lấy dấu hướng X

		while (elapsedTime < timer)
		{
			// Tăng thời gian đã trôi qua
			elapsedTime += Time.deltaTime;
			if(isScaleUp && elapsedTime >= timer / 2)
			{
				cam.cullingMask = -1;
			}
			// Tính toán giá trị Y hiện tại sử dụng Lerp
			float newY = Mathf.Lerp(startY, tagertY, elapsedTime / timer);
			Vector3 newScale = Vector3.Lerp(startScale, endScale, elapsedTime / timer);
			float newOrthographic = Mathf.Lerp(startOrthographic, endOrthographic, elapsedTime / timer);

			// Cập nhật vị trí của đối tượng
			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
			transform.localScale = newScale;
			virtualCamera.m_Lens.OrthographicSize = newOrthographic;

			// Chờ một frame trước khi tiếp tục
			yield return null;
		}

		// Đảm bảo đối tượng đạt đến vị trí Y mục tiêu
		transform.position = new Vector3(transform.position.x, tagertY, transform.position.z);
		transform.localScale = endScale;
		virtualCamera.m_Lens.OrthographicSize = endOrthographic;


		if (isScaleUp)
		{
			playerOrderLayer.sortingOrder = 0;
		}
		else
		{
			playerOrderLayer.sortingOrder = -3;
			int middlegroundLayer = LayerMask.NameToLayer("MiddleGround");
			cam.cullingMask = ~(1 << middlegroundLayer);
		}
	}
}
