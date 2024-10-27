using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
	public Camera mainCamera;

	void Start()
	{
		// Nếu không có camera nào được gán, lấy camera chính
		if (mainCamera == null)
		{
			mainCamera = Camera.main;
		}

		// Điều chỉnh kích thước background ban đầu
		AdjustBackgroundSize();
	}

	void Update()
	{
		// Kiểm tra nếu orthographicSize đã thay đổi và điều chỉnh lại kích thước
		AdjustBackgroundSize();
	}

	void AdjustBackgroundSize()
	{
		// Tính toán kích thước mới cho background
		float height = mainCamera.orthographicSize * 2; 
		float width = height * mainCamera.aspect;

		

		// Lấy kích thước hiện tại của sprite
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		float spriteWidth = spriteRenderer.sprite.bounds.size.x;
		float spriteHeight = spriteRenderer.sprite.bounds.size.y;

		// Tính tỷ lệ cần điều chỉnh
		float scaleX = width / spriteWidth;
		float scaleY = height / spriteHeight;

		// Chọn tỷ lệ lớn hơn để đảm bảo lấp đầy khung camera
		float scale = Mathf.Max(scaleX, scaleY);

		// Đặt kích thước cho background
		transform.localScale = new Vector3(scale, scale, 1);

		// Đặt vị trí background ở giữa camera
		transform.position = new Vector3(mainCamera.transform.position.x,
										  mainCamera.transform.position.y,
										  transform.position.z);
	}

}
