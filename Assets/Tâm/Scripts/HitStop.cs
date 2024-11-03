using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	public static HitStop instance;
	public float duration = 0.1f; // Thời gian tạm dừng (có thể tùy chỉnh)
	private bool isStopping = false; // Kiểm tra trạng thái HitStop

	private void Awake()
	{
		instance = this;
	}

	// Hàm gọi để thực hiện hiệu ứng HitStop
	public void Stop()
	{
		if (!isStopping)
		{
			StartCoroutine(HitStopCoroutine());
		}
	}

	private IEnumerator HitStopCoroutine()
	{
		isStopping = true;

		// Lưu lại thời gian TimeScale gốc để khôi phục lại sau đó
		float originalTimeScale = Time.timeScale;

		// Dừng thời gian (hoặc làm chậm)
		Time.timeScale = 0f; // hoặc thử Time.timeScale = 0.2f để làm chậm thay vì dừng hẳn
		yield return new WaitForSecondsRealtime(duration); // Sử dụng WaitForSecondsRealtime để không bị ảnh hưởng bởi Time.timeScale

		// Khôi phục lại TimeScale
		Time.timeScale = originalTimeScale;

		isStopping = false;
	}
}
